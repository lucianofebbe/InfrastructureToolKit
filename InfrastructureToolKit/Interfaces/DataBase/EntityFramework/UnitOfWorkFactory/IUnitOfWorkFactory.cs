using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.DataBase.EntityFramework.UnitOfWork;
using InfrastructureToolKit.Settings.DataBases.EntityFramework.Settings;

namespace InfrastructureToolKit.Interfaces.DataBase.EntityFramework.UnitOfWorkFactory
{
    // Interface para fábrica que cria instâncias de IUnitOfWork<T> para Entity Framework
    public interface IUnitOfWorkFactory<T>
        where T : BaseEntitiesSql
    {
        // Cria uma Unidade de Trabalho com contexto existente, podendo iniciar uma transação e aceitar token de cancelamento
        Task<IUnitOfWork<T>> Create(ConnectionSettings connectionSettings);
    }
}
