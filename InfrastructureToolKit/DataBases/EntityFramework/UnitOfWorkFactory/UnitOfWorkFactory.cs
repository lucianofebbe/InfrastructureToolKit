using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.EntityFramework.Settings;
using InfrastructureToolKit.DataBase.EntityFramework.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.EntityFramework.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.EntityFramework.UnitOfWorkFactory;

namespace InfrastructureToolKit.DataBase.EntityFramework.UnitOfWorkFactory
{
    public class UnitOfWorkFactory<T> : IUnitOfWorkFactory<T>
        where T : BaseEntitiesSql
    {
        public virtual async Task<IUnitOfWork<T>> Create(ConnectionSettings connectionSettings)
        {
            IUnitOfWork<T> unit = new UnitOfWork<T>(connectionSettings);
            if (connectionSettings.BeginTransactionAsync)
                await unit.BeginTransactionAsync();
            return unit;
        }
    }
}
