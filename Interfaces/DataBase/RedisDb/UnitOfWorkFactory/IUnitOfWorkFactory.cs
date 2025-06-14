using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.RedisDb.Settings;
using InfrastructureToolKit.Interfaces.DataBase.RedisDb.UnitOfWork;

namespace InfrastructureToolKit.Interfaces.DataBase.RedisDb.UnitOfWorkFactory
{
    public interface IUnitOfWorkFactory<T> where T : BaseEntitiesRedisDb
    {
        /// <summary>
        /// Cria uma instância de IUnitOfWork utilizando configurações customizadas do Redis, 
        /// com tempo opcional de expiração e renovação de itens.
        /// </summary>
        /// <param name="configurationOptions">Opções de configuração do Redis.</param>
        /// <param name="expireItem">Tempo para expiração do item no cache (opcional).</param>
        /// <param name="renewItem">Tempo para renovação automática do item no cache (opcional).</param>
        Task<IUnitOfWork<T>> Create(ConnectionSettings connectionSettings);
    }
}
