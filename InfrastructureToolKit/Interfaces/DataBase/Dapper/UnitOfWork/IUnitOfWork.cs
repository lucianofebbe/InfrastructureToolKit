using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Settings.DataBases.Dapper.Settings;

namespace InfrastructureToolKit.Interfaces.DataBase.Dapper.UnitOfWork
{
    // Interface de Unidade de Trabalho para Dapper, responsável por operações de CRUD e controle de transações
    public interface IUnitOfWork<T> : IDisposable
        where T : BaseEntitiesSql
    {
        // Insere a entidade no banco de dados utilizando SQL customizado
        Task<T> InsertAsync(CommandSettings<T> commandSettings);

        // Atualiza a entidade no banco de dados utilizando SQL customizado
        Task<T> UpdateAsync(CommandSettings<T> commandSettings);

        // Exclui a entidade do banco de dados
        Task<bool> DeleteAsync(CommandSettings<T> commandSettings);

        // Busca uma entidade específica com parâmetros via SQL
        Task<T> GetAsync(CommandSettings<T> commandSettings);

        // Retorna uma lista paginada de entidades com base no SQL e parâmetros fornecidos
        Task<List<T>> GetAllAsync(CommandSettings<T> commandSettings);

        // Inicia uma transação no contexto atual
        Task BeginTransactionAsync();

        // Efetiva as alterações realizadas durante a transação
        Task<bool> CommitAsync();
    }
}
