using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.Dapper.Settings;
using InfrastructureToolKit.DataBase.Dapper.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.Dapper.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.Dapper.UnitOfWorkFactory;

namespace InfrastructureToolKit.DataBase.Dapper.UnitOfWorkFactory
{
    public class UnitOfWorkFactory<T> : IUnitOfWorkFactory<T>
        where T : BaseEntitiesSql
    {
        // Cria UnitOfWork usando conexão fornecida e inicia transação se solicitado
        public virtual async Task<IUnitOfWork<T>> Create(ConnectionSettings connectionSettings)
        {
            IUnitOfWork<T> unit = new UnitOfWork<T>(connectionSettings);
            if (connectionSettings.EnableTransaction)
                await unit.BeginTransactionAsync();
            return unit;
        }
    }
}
