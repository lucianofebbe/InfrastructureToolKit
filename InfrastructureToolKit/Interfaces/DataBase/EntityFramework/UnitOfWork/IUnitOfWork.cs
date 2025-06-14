using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.EntityFramework.Settings;
using System.Linq.Expressions;

namespace InfrastructureToolKit.Interfaces.DataBase.EntityFramework.UnitOfWork
{
    // Interface genérica para Unidade de Trabalho usando Entity Framework
    // Define operações básicas assíncronas para CRUD com suporte a transações
    public interface IUnitOfWork<T> : IAsyncDisposable
        where T : BaseEntitiesSql
    {
        // Busca uma entidade que satisfaça o predicado
        // noTracking desabilita rastreamento para consultas de leitura
        // deleteds indica se considera entidades "deletadas" logicamente
        Task<T> GetAsync(CommandSettings<T> commandSettings);

        // Busca lista de entidades com filtro opcional e paginação
        Task<List<T>> GetAllAsync(CommandSettings<T> commandSettings);

        // Insere uma nova entidade (sem salvar alterações no banco)
        Task<T> InsertAsync(T entidade);

        // Atualiza uma entidade existente (sem salvar alterações no banco)
        Task<T> UpdateAsync(T entidade);

        // Deleta uma entidade (sem salvar alterações no banco)
        Task<bool> DeleteAsync(CommandSettings<T> commandSettings);

        // Insere e salva imediatamente no banco
        Task<T> InsertAndSaveAsync(CommandSettings<T> commandSettings);

        // Atualiza e salva imediatamente no banco
        Task<T> UpdateAndSaveAsync(CommandSettings<T> commandSettings);

        // Deleta e salva imediatamente no banco
        Task<bool> DeleteAndSaveAsync(CommandSettings<T> commandSettings);

        // Inicia uma transação
        Task BeginTransactionAsync();

        // Confirma as alterações da transação
        Task<bool> CommitAsync(CommandSettings<T> commandSettings);
    }
}
