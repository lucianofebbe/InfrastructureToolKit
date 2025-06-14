using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.AdoNet.Settings;
using InfrastructureToolKit.Interfaces.DataBase.AdoNet.Ado;

namespace InfrastructureToolKit.Interfaces.DataBase.AdoNet.AdoFactory
{
    // Interface para a fábrica de instâncias ADO, permitindo criar objetos IAdo<T> com ou sem configurações específicas
    public interface IAdoFactory<T> where T : BaseEntitiesSql
    {
        // Cria uma instância de IAdo<T> com configurações fornecidas
        Task<IAdo<T>> Create(ConnectionSettings connectionSettings);
    }
}
