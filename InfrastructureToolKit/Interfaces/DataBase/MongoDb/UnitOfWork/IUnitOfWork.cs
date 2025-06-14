using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.MongoDb.Settings;

namespace InfrastructureToolKit.Interfaces.DataBase.MongoDb.UnitOfWork
{
    // Interface para Unidade de Trabalho no MongoDB para entidades derivadas de BaseEntitiesMongoDb
    public interface IUnitOfWork<T> where T : BaseEntitiesMongoDb
    {
        // Insere um documento no MongoDB e retorna o ID gerado
        Task<T> InsertAsync(CommandSettings<T> CommandSettings);

        // Atualiza um documento existente, retorna true se sucesso
        Task<bool> UpdateAsync(CommandSettings<T> CommandSettings);

        // Deleta um documento, retorna true se sucesso
        Task<bool> DeleteAsync(CommandSettings<T> CommandSettings);

        // Obtém um único documento baseado em um filtro MongoDB
        Task<T> GetAsync(CommandSettings<T> CommandSettings);

        // Obtém uma lista de documentos com filtro opcional
        Task<List<T>> GetAllAsync(CommandSettings<T> CommandSettings);
    }
}
