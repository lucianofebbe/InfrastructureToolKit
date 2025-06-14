using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.MongoDb.Settings;
using InfrastructureToolKit.DataBase.MongoDb.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.MongoDb.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.MongoDb.UnitOfWorkFactory;

namespace InfrastructureToolKit.DataBase.MongoDb.UnitOfWorkFactory
{
    public class UnitOfWorkFactory<T> : IUnitOfWorkFactory<T> where T : BaseEntitiesMongoDb
    {
        // Cria uma instância do UnitOfWork usando uma configuração passada como parâmetro
        public virtual async Task<IUnitOfWork<T>> Create(ConnectionSettings config)
        {
            IUnitOfWork<T> mongoDb = new UnitOfWork<T>(config);
            return mongoDb;
        }
    }
}
