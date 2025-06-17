using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.DataBase.Dapper.UnitOfWork;
using InfrastructureToolKit.Settings.DataBases.Dapper.Settings;

namespace InfrastructureToolKit.Interfaces.DataBase.Dapper.UnitOfWorkFactory
{
    // Interface para fábrica de Unidade de Trabalho Dapper
    // Responsável por criar instâncias de IUnitOfWork<T> com ou sem conexão fornecida
    public interface IUnitOfWorkFactory<T>
        where T : BaseEntitiesSql
    {
        // Cria uma Unidade de Trabalho utilizando uma conexão existente,
        // opcionalmente iniciando uma transação e aceitando cancelamento
        Task<IUnitOfWork<T>> Create(ConnectionSettings settings);
    }
}
