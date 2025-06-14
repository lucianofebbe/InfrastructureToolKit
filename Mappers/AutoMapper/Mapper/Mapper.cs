using AutoMapper;
using InfrastructureToolKit.Bases.Dtos;
using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper;
using Newtonsoft.Json;

namespace InfrastructureToolKit.Mappers.AutoMapper.Mapper
{
    // Classe genérica Mapper para conversão entre diferentes tipos de entidades e DTOs
    // Utiliza AutoMapper para mapear entre entidades SQL, MongoDB, Redis, Requests e Responses
    [Obsolete("Use MapperSpecific em vez de Mapper.")]
    public class Mapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response> :
        IMapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>
     where EntitySql : BaseEntitiesSql
     where EntityMongoDb : BaseEntitiesMongoDb
     where EntityRedisDb : BaseEntitiesRedisDb
     where Request : BaseRequest
     where Response : BaseResponse
    {
        private readonly IMapper mapper;

        // Construtor recebe perfis do AutoMapper para configuração das regras de mapeamento
        public Mapper(IEnumerable<Profile> profiles)
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                    cfg.AddProfile(profile);
            });

            mapper = config.CreateMapper();
        }

        // Métodos de mapeamento entre entidades MongoDB e Redis
        public virtual async Task<EntityRedisDb> EntityMongoDbToEntityRedisDbAsync(EntityMongoDb item)
        {
            return await Task.FromResult(mapper.Map<EntityRedisDb>(item));
        }
        public virtual async Task<List<EntityRedisDb>> EntityMongoDbToEntityRedisDbAsync(List<EntityMongoDb> item)
        {
            return await Task.FromResult(mapper.Map<List<EntityRedisDb>>(item));
        }

        // Métodos de mapeamento entre entidades MongoDB e SQL
        public virtual async Task<EntitySql> EntityMongoDbToEntitySqlAsync(EntityMongoDb item)
        {
            return await Task.FromResult(mapper.Map<EntitySql>(item));
        }
        public virtual async Task<List<EntitySql>> EntityMongoDbToEntitySqlAsync(List<EntityMongoDb> item)
        {
            return await Task.FromResult(mapper.Map<List<EntitySql>>(item));
        }

        // Métodos para serializar entidades MongoDB em JSON
        public virtual async Task<string> EntityMongoDbToJsonAsync(EntityMongoDb item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<string> EntityMongoDbToJsonAsync(List<EntityMongoDb> item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }

        // Métodos para converter entidade MongoDB para Request e Response (e suas listas)
        public virtual async Task<Request> EntityMongoDbToRequestAsync(EntityMongoDb item)
        {
            return await Task.FromResult(mapper.Map<Request>(item));
        }
        public virtual async Task<List<Request>> EntityMongoDbToRequestAsync(List<EntityMongoDb> item)
        {
            return await Task.FromResult(mapper.Map<List<Request>>(item));
        }
        public virtual async Task<Response> EntityMongoDbToResponseAsync(EntityMongoDb item)
        {
            return await Task.FromResult(mapper.Map<Response>(item));
        }
        public virtual async Task<List<Response>> EntityMongoDbToResponseAsync(List<EntityMongoDb> item)
        {
            return await Task.FromResult(mapper.Map<List<Response>>(item));
        }

        // Métodos para converter entidades Redis para MongoDB, SQL, Request e Response (e suas listas)
        public virtual async Task<EntityMongoDb> EntityRedisDbToEntityMongoDbAsync(EntityRedisDb item)
        {
            return await Task.FromResult(mapper.Map<EntityMongoDb>(item));
        }
        public virtual async Task<List<EntityMongoDb>> EntityRedisDbToEntityMongoDbAsync(List<EntityRedisDb> item)
        {
            return await Task.FromResult(mapper.Map<List<EntityMongoDb>>(item));
        }
        public virtual async Task<EntitySql> EntityRedisDbToEntitySqlAsync(EntityRedisDb item)
        {
            return await Task.FromResult(mapper.Map<EntitySql>(item));
        }
        public virtual async Task<List<EntitySql>> EntityRedisDbToEntitySqlAsync(List<EntityRedisDb> item)
        {
            return await Task.FromResult(mapper.Map<List<EntitySql>>(item));
        }
        public virtual async Task<string> EntityRedisDbToJsonAsync(EntityRedisDb item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<string> EntityRedisDbToJsonAsync(List<EntityRedisDb> item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<Request> EntityRedisDbToRequestAsync(EntityRedisDb item)
        {
            return await Task.FromResult(mapper.Map<Request>(item));
        }
        public virtual async Task<List<Request>> EntityRedisDbToRequestAsync(List<EntityRedisDb> item)
        {
            return await Task.FromResult(mapper.Map<List<Request>>(item));
        }
        public virtual async Task<Response> EntityRedisDbToResponseAsync(EntityRedisDb item)
        {
            return await Task.FromResult(mapper.Map<Response>(item));
        }
        public virtual async Task<List<Response>> EntityRedisDbToResponseAsync(List<EntityRedisDb> item)
        {
            return await Task.FromResult(mapper.Map<List<Response>>(item));
        }

        // Métodos para converter entidades SQL para MongoDB, Redis, Request e Response (e suas listas)
        public virtual async Task<EntityMongoDb> EntitySqlToEntityMongoDbAsync(EntitySql item)
        {
            return await Task.FromResult(mapper.Map<EntityMongoDb>(item));
        }
        public virtual async Task<List<EntityMongoDb>> EntitySqlToEntityMongoDbAsync(List<EntitySql> item)
        {
            return await Task.FromResult(mapper.Map<List<EntityMongoDb>>(item));
        }
        public virtual async Task<EntityRedisDb> EntitySqlToEntityRedisDbAsync(EntitySql item)
        {
            return await Task.FromResult(mapper.Map<EntityRedisDb>(item));
        }
        public virtual async Task<List<EntityRedisDb>> EntitySqlToEntityRedisDbAsync(List<EntitySql> item)
        {
            return await Task.FromResult(mapper.Map<List<EntityRedisDb>>(item));
        }
        public virtual async Task<string> EntitySqlToJsonAsync(EntitySql item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<string> EntitySqlToJsonAsync(List<EntitySql> item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<Request> EntitySqlToRequestAsync(EntitySql item)
        {
            return await Task.FromResult(mapper.Map<Request>(item));
        }
        public virtual async Task<List<Request>> EntitySqlToRequestAsync(List<EntitySql> item)
        {
            return await Task.FromResult(mapper.Map<List<Request>>(item));
        }
        public virtual async Task<Response> EntitySqlToResponseAsync(EntitySql item)
        {
            return await Task.FromResult(mapper.Map<Response>(item));
        }
        public virtual async Task<List<Response>> EntitySqlToResponseAsync(List<EntitySql> item)
        {
            return await Task.FromResult(mapper.Map<List<Response>>(item));
        }

        // Métodos para converter JSON em entidades MongoDB, Redis, SQL, Request e Response
        public virtual async Task<EntityMongoDb> JsonToEntityMongoDbAsync(string item)
        {
            return await Task.FromResult(mapper.Map<EntityMongoDb>(item));
        }
        public virtual async Task<List<EntityMongoDb>> JsonToEntityMongoDbListAsync(string item)
        {
            return await Task.FromResult(mapper.Map<List<EntityMongoDb>>(item));
        }
        public virtual async Task<EntityRedisDb> JsonToEntityRedisDbAsync(string item)
        {
            return await Task.FromResult(mapper.Map<EntityRedisDb>(item));
        }
        public virtual async Task<List<EntityRedisDb>> JsonToEntityRedisDbListAsync(string item)
        {
            return await Task.FromResult(mapper.Map<List<EntityRedisDb>>(item));
        }
        public virtual async Task<EntitySql> JsonToEntitySqlAsync(string item)
        {
            return await Task.FromResult(mapper.Map<EntitySql>(item));
        }
        public virtual async Task<List<EntitySql>> JsonToEntitySqlListAsync(string item)
        {
            return await Task.FromResult(mapper.Map<List<EntitySql>>(item));
        }
        public virtual async Task<Request> JsonToRequestAsync(string item)
        {
            return await Task.FromResult(mapper.Map<Request>(item));
        }
        public virtual async Task<List<Request>> JsonToRequestListAsync(string item)
        {
            return await Task.FromResult(mapper.Map<List<Request>>(item));
        }
        public virtual async Task<Response> JsonToResponseAsync(string item)
        {
            return await Task.FromResult(mapper.Map<Response>(item));
        }
        public virtual async Task<List<Response>> JsonToResponseListAsync(string item)
        {
            return await Task.FromResult(mapper.Map<List<Response>>(item));
        }

        // Métodos para converter Request para entidades MongoDB, Redis, SQL e Response
        public virtual async Task<EntityMongoDb> RequestToEntityMongoDbAsync(Request item)
        {
            return await Task.FromResult(mapper.Map<EntityMongoDb>(item));
        }
        public virtual async Task<List<EntityMongoDb>> RequestToEntityMongoDbAsync(List<Request> item)
        {
            return await Task.FromResult(mapper.Map<List<EntityMongoDb>>(item));
        }
        public virtual async Task<EntityRedisDb> RequestToEntityRedisDbAsync(Request item)
        {
            return await Task.FromResult(mapper.Map<EntityRedisDb>(item));
        }
        public virtual async Task<List<EntityRedisDb>> RequestToEntityRedisDbAsync(List<Request> item)
        {
            return await Task.FromResult(mapper.Map<List<EntityRedisDb>>(item));
        }
        public virtual async Task<EntitySql> RequestToEntitySqlAsync(Request item)
        {
            return await Task.FromResult(mapper.Map<EntitySql>(item));
        }
        public virtual async Task<List<EntitySql>> RequestToEntitySqlAsync(List<Request> item)
        {
            return await Task.FromResult(mapper.Map<List<EntitySql>>(item));
        }
        public virtual async Task<string> RequestToJsonAsync(Request item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<string> RequestToJsonAsync(List<Request> item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<Response> RequestToResponseAsync(Request item)
        {
            return await Task.FromResult(mapper.Map<Response>(item));
        }
        public virtual async Task<List<Response>> RequestToResponseAsync(List<Request> item)
        {
            return await Task.FromResult(mapper.Map<List<Response>>(item));
        }

        // Métodos para converter Response para entidades MongoDB, Redis, SQL e Request
        public virtual async Task<EntityMongoDb> ResponseToEntityMongoDbAsync(Response item)
        {
            return await Task.FromResult(mapper.Map<EntityMongoDb>(item));
        }
        public virtual async Task<List<EntityMongoDb>> ResponseToEntityMongoDbAsync(List<Response> item)
        {
            return await Task.FromResult(mapper.Map<List<EntityMongoDb>>(item));
        }
        public virtual async Task<EntityRedisDb> ResponseToEntityRedisDbAsync(Response item)
        {
            return await Task.FromResult(mapper.Map<EntityRedisDb>(item));
        }
        public virtual async Task<List<EntityRedisDb>> ResponseToEntityRedisDbAsync(List<Response> item)
        {
            return await Task.FromResult(mapper.Map<List<EntityRedisDb>>(item));
        }
        public virtual async Task<EntitySql> ResponseToEntitySqlAsync(Response item)
        {
            return await Task.FromResult(mapper.Map<EntitySql>(item));
        }
        public virtual async Task<List<EntitySql>> ResponseToEntitySqlAsync(List<Response> item)
        {
            return await Task.FromResult(mapper.Map<List<EntitySql>>(item));
        }
        public virtual async Task<string> ResponseToJsonAsync(Response item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<string> ResponseToJsonAsync(List<Response> item)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(item));
        }
        public virtual async Task<Request> ResponseToRequestAsync(Response item)
        {
            return await Task.FromResult(mapper.Map<Request>(item));
        }
        public virtual async Task<List<Request>> ResponseToRequestAsync(List<Response> item)
        {
            return await Task.FromResult(mapper.Map<List<Request>>(item));
        }
    }
}
