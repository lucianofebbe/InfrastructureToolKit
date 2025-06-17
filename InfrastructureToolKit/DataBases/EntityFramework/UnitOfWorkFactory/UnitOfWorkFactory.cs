using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBases.EntityFramework.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.EntityFramework.UnitOfWork;
using InfrastructureToolKit.Interfaces.DataBase.EntityFramework.UnitOfWorkFactory;
using InfrastructureToolKit.Settings.DataBases.EntityFramework.Settings;

namespace InfrastructureToolKit.DataBases.EntityFramework.UnitOfWorkFactory
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
