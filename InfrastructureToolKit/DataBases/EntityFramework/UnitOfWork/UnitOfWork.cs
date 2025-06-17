using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.DataBase.EntityFramework.UnitOfWork;
using InfrastructureToolKit.Settings.DataBases.EntityFramework.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InfrastructureToolKit.DataBases.EntityFramework.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T>
        where T : BaseEntitiesSql
    {
        private IDbContextTransaction transaction;
        private ConnectionSettings connectionSettings;
        private bool committed;

        // Construtor recebe contexto EF e token de cancelamento
        public UnitOfWork(ConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings;
        }

        // Inicia uma transação assíncrona no banco
        public virtual async Task BeginTransactionAsync()
        {
            transaction = await connectionSettings.Context.Database.BeginTransactionAsync();
        }

        // Obtém uma entidade com filtro, opcional sem rastreamento e filtro para registros deletados
        public virtual async Task<T> GetAsync(CommandSettings<T> commandSettings)
        {
            var query = connectionSettings.Context.Set<T>().AsQueryable();
            query = query.Where(commandSettings.Predicate);

            if (commandSettings.Deleteds != null)
                query = query.Where(del => del.Deleted == commandSettings.Deleteds);
            else
                query = query.Where(del => del.Deleted == false);

            if (commandSettings.NoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(commandSettings.CancellationToken);
        }

        // Obtém lista de entidades com filtros, paginação e opções de tracking e filtro de deletados
        public virtual async Task<List<T>> GetAllAsync(CommandSettings<T> commandSettings)
        {
            var query = connectionSettings.Context.Set<T>().AsQueryable();

            if (commandSettings.Predicate != null)
                query = query.Where(commandSettings.Predicate);

            if (commandSettings.Deleteds != null)
                query = query.Where(del => del.Deleted == commandSettings.Deleteds);
            else
                query = query.Where(del => del.Deleted == false);

            if (commandSettings.NoTracking)
                query = query.AsNoTracking();

            if (commandSettings.Skip > 0)
                query = query.Skip(commandSettings.Skip);

            if (commandSettings.Take > 0)
                query = query.Take(commandSettings.Take);

            return await query.ToListAsync(commandSettings.CancellationToken);
        }

        // Marca entidade para inserção, ajusta campos padrão (Guid, Created, Updated)
        public virtual async Task<T> InsertAsync(T entidade)
        {
            entidade.Guid = Guid.NewGuid();
            entidade.Created = DateTime.UtcNow;
            entidade.Updated = DateTime.UtcNow;
            connectionSettings.Context.Entry(entidade).State = EntityState.Added;
            return entidade;
        }

        // Marca entidade para atualização, atualiza campo Updated
        public virtual async Task<T> UpdateAsync(T entidade)
        {
            entidade.Updated = DateTime.UtcNow;
            connectionSettings.Context.Entry(entidade).State = EntityState.Modified;
            return entidade;
        }

        // Marca entidade como deletada (soft delete) buscando-a primeiro pelo Id ou Guid
        public virtual async Task<bool> DeleteAsync(CommandSettings<T> commandSettings)
        {
            var resultFind = Activator.CreateInstance<T>();

            if (commandSettings.Entity.Id > 0 && commandSettings.Entity.Guid != Guid.Empty)
                throw new InvalidOperationException("Only one identifier (Id or Guid) should be provided for deletion.");

            if (commandSettings.Entity.Id > 0)
            {
                commandSettings.Predicate = a => a.Id == commandSettings.Entity.Id;
                resultFind = await GetAsync(commandSettings);
            }

            if (commandSettings.Entity.Guid != Guid.Empty)
            {
                commandSettings.Predicate = a => a.Guid == commandSettings.Entity.Guid;
                resultFind = await GetAsync(commandSettings);
            }

            if (resultFind.Id > 0)
            {
                resultFind.Deleted = true;
                var updated = await UpdateAsync(resultFind);
                return updated.Id > 0;
            }

            return false;
        }

        // Insere entidade e salva mudanças no banco
        public virtual async Task<T> InsertAndSaveAsync(CommandSettings<T> commandSettings)
        {
            var result = await InsertAsync(commandSettings.Entity);
            await connectionSettings.Context.SaveChangesAsync(commandSettings.CancellationToken);
            return result;
        }

        // Atualiza entidade e salva mudanças no banco
        public virtual async Task<T> UpdateAndSaveAsync(CommandSettings<T> commandSettings)
        {
            var result = await UpdateAsync(commandSettings.Entity);
            await connectionSettings.Context.SaveChangesAsync(commandSettings.CancellationToken);
            return result;
        }

        // Deleta (soft delete) entidade e salva mudanças no banco
        public virtual async Task<bool> DeleteAndSaveAsync(CommandSettings<T> commandSettings)
        {
            var result = await DeleteAsync(commandSettings);
            if (result)
                await connectionSettings.Context.SaveChangesAsync(commandSettings.CancellationToken);
            return result;
        }

        // Salva as alterações pendentes e realiza commit da transação, se houver
        public virtual async Task<bool> CommitAsync(CommandSettings<T> commandSettings)
        {
            await connectionSettings.Context.SaveChangesAsync(commandSettings.CancellationToken);

            if (transaction != null)
                await transaction.CommitAsync(commandSettings.CancellationToken);

            committed = true;
            return true;
        }

        // Desfaz transação se não foi comitada, e descarta recursos
        public virtual async ValueTask DisposeAsync()
        {
            if (transaction != null && !committed)
                await transaction.RollbackAsync();

            if (transaction != null)
                await transaction.DisposeAsync();

            await connectionSettings.Context.DisposeAsync();
        }
    }
}
