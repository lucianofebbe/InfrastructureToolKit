using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.RedisDb.Settings;
using InfrastructureToolKit.DataBase.RedisDb.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.RedisDb.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.RedisDb.UnitOfWorkFactory;

namespace InfrastructureToolKit.DataBase.RedisDb.UnitOfWorkFactory
{
    public class UnitOfWorkFactory<T> : IUnitOfWorkFactory<T> where T : BaseEntitiesRedisDb
    {
        // Cria uma instância de IUnitOfWork<T> usando as opções de configuração passadas diretamente
        public virtual async Task<IUnitOfWork<T>> Create(ConnectionSettings connectionSettings)
        {
            // Cria o UnitOfWork com conexão e parâmetros opcionais
            IUnitOfWork<T> redis = new UnitOfWork<T>(connectionSettings);
            return redis;
        }
    }
}
