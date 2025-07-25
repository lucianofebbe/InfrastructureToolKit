﻿using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.DataBase.RedisDb.UnitOfWork;
using InfrastructureToolKit.Settings.DataBases.RedisDb.Settings;
using Newtonsoft.Json;
using NReJSON;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Reflection;

namespace InfrastructureToolKit.DataBases.RedisDb.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : BaseEntitiesRedisDb
    {
        private readonly IDatabase db;
        private readonly string keyPrefix;
        private readonly string indexName;
        private ConnectionSettings connectionSettings;
        private ConnectionMultiplexer connectionMultiplexer;
        public UnitOfWork(ConnectionSettings connectionSettings)
        {
            var config = new ConfigurationOptions()
            {
                Password = connectionSettings.Password,
                AbortOnConnectFail = connectionSettings.AbortOnConnectFail,
            };

            foreach (var item in connectionSettings.EndPoints)
                config.EndPoints.Add(item);

            connectionMultiplexer = ConnectionMultiplexer.Connect(config);
            this.connectionSettings = connectionSettings;
            db = connectionMultiplexer.GetDatabase();
            keyPrefix = typeof(T).Name + ":";
            indexName = typeof(T).Name + "_idx";

            EnsureIndexCreated(); // Garante que o índice de busca esteja criado no Redis
        }

        public virtual async Task<Guid> InsertAsync(CommandSettings<T> commandSettings)
        {
            commandSettings.Entity.Guid = Guid.NewGuid();

            RedisKey key = keyPrefix + commandSettings.Entity.Guid;

            await db.JsonSetAsync(key, commandSettings.Entity); // Serializa e armazena o objeto como JSON no Redis

            if (commandSettings.ExpireItem.HasValue)
                await db.KeyExpireAsync(key, commandSettings.ExpireItem);

            return commandSettings.Entity.Guid;
        }

        public virtual async Task<bool> UpdateAsync(CommandSettings<T> commandSettings)
        {
            RedisKey key = keyPrefix + commandSettings.Entity.Guid;

            await db.JsonSetAsync(key, commandSettings.Entity); // Atualiza o JSON existente

            if (commandSettings.ExpireItem.HasValue)
                await db.KeyExpireAsync(key, commandSettings.ExpireItem);

            return true;
        }

        public virtual async Task<bool> DeleteAsync(CommandSettings<T> commandSettings)
        {
            var key = keyPrefix + commandSettings.Entity.Guid;
            return await db.KeyDeleteAsync(key); // Remove a chave do Redis
        }

        public virtual async Task<T?> GetAsync(CommandSettings<T> commandSettings)
        {
            var key = keyPrefix + commandSettings.Entity.Guid;
            var result = await db.ExecuteAsync("JSON.GET", key);

            if (result is { IsNull: false })
            {
                if (commandSettings.RenewItem.HasValue)
                    await db.KeyExpireAsync(key, commandSettings.RenewItem.Value);

                if (commandSettings.DeleteAfterReader)
                    await DeleteAsync(commandSettings);

                return JsonConvert.DeserializeObject<T>(result.ToString());
            }
            return null;
        }

        private void EnsureIndexCreated()
        {
            var server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints().First());
            var indexesResult = server.Execute("FT._LIST");

            var indexList = new List<string>();

            if (indexesResult.Type == ResultType.MultiBulk)
            {
                foreach (var item in (RedisResult[])indexesResult)
                    indexList.Add(item.ToString());
            }

            if (indexList.Contains(indexName))
                return;

            var args = new List<object>
            {
                indexName,
                "ON", "JSON",
                "PREFIX", "1", keyPrefix,
                "SCHEMA"
            };

            // Monta o schema do índice com base nas propriedades de T
            foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var jsonName = prop.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? prop.Name;
                var redisJsonPath = $"$.{jsonName}";
                var alias = jsonName;
                var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                string redisType = propType switch
                {
                    Type t when t == typeof(int) || t == typeof(long) || t == typeof(float)
                              || t == typeof(double) || t == typeof(decimal) || t == typeof(DateTime)
                        => "NUMERIC",

                    Type t when t == typeof(bool) || t.IsEnum
                        => "TAG",

                    _ => "TEXT"
                };

                args.Add(redisJsonPath);
                args.Add("AS");
                args.Add(alias);
                args.Add(redisType);
            }

            try
            {
                db.Execute("FT.CREATE", args.ToArray());
            }
            catch (RedisServerException ex) when (ex.Message.Contains("Index already exists"))
            {
                // Se o índice já existe, ignora a exceção
            }
        }

        private List<T> ParseSearchResult(RedisResult result)
        {
            var list = new List<T>();

            if (result == null || result.Type != ResultType.MultiBulk)
                return list;

            var results = (RedisResult[])result;

            if (results.Length < 3)
                return list;

            for (int i = 1; i < results.Length; i += 2)
            {
                if (i + 1 >= results.Length) break;

                var raw = results[i + 1];

                if (raw.Type == ResultType.MultiBulk)
                {
                    var inner = (RedisResult[])raw;

                    if (inner.Length >= 2 && inner[0].ToString() == "$")
                    {
                        string json = inner[1]?.ToString();
                        if (!string.IsNullOrEmpty(json))
                        {
                            try
                            {
                                var obj = JsonConvert.DeserializeObject<T>(json);
                                if (obj != null)
                                    list.Add(obj);
                            }
                            catch { }
                        }
                    }
                }
            }

            return list;
        }

        private string GetAliasName(MemberExpression member)
        {
            if (member.Member is PropertyInfo prop)
            {
                var jsonAttr = prop.GetCustomAttribute<JsonPropertyAttribute>();
                return jsonAttr?.PropertyName ?? prop.Name;
            }

            return member.Member.Name;
        }
    }
}
