using Dapper;
using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.DataBase.Dapper.UnitOfWork;
using InfrastructureToolKit.Settings.DataBases.Dapper.Settings;
using System.Data;

namespace InfrastructureToolKit.DataBases.Dapper.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : BaseEntitiesSql
    {
        // Conexão com o banco de dados, injetada via construtor
        private readonly IDbConnection connection;

        // Transação ativa para controle de commit/rollback
        private IDbTransaction transaction;

        // Flag para indicar se a transação foi comitada
        private bool _committed;

        // Construtor que recebe a conexão e o CancellationToken, já abre a conexão
        public UnitOfWork(ConnectionSettings settings)
        {
            connection = settings.Connection;

            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        // Insere uma entidade usando o SQL passado, retorna a entidade com o Id atualizado
        public virtual async Task<object> InsertAsync(CommandSettings<T> commandSettings)
        {
            object result;
            if (commandSettings.ExecuteScalar)
                result = await connection.ExecuteScalarAsync(commandSettings.Query, commandSettings.Parameters, transaction, commandType: GetCommand(commandSettings));
            else
                result = await connection.ExecuteAsync(commandSettings.Query, commandSettings.Parameters, transaction, commandType: GetCommand(commandSettings));

            Dispose();

            return result;
        }

        // Atualiza uma entidade no banco de dados e retorna a entidade atualizada
        public virtual async Task<T> UpdateAsync(CommandSettings<T> commandSettings)
        {
            await connection
                .ExecuteAsync(commandSettings.Query, commandSettings.Parameters, transaction, commandType: GetCommand(commandSettings));
            Dispose();
            return commandSettings.Entity;
        }

        // Marca a entidade como deletada (soft delete) com base em Guid ou Id
        public virtual async Task<bool> DeleteSoftAsync(CommandSettings<T> commandSettings)
        {
            var guidOrId = commandSettings.Entity.Guid != Guid.Empty ? "Guid = @Guid" : "Id = @Id";
            var sql = $"UPDATE {typeof(T).Name} SET Deleted = 1, Updated = @Updated WHERE {guidOrId}";

            var parameters = new { commandSettings.Entity.Guid, commandSettings.Entity.Id, Updated = DateTime.UtcNow };
            var affected = await connection.ExecuteAsync(sql, parameters, transaction, commandType: GetCommand(commandSettings));
            Dispose();
            return affected > 0;
        }

        public virtual async Task<bool> DeleteAsync(CommandSettings<T> commandSettings)
        {
            var guidOrId = commandSettings.Entity.Guid != Guid.Empty ? "Guid = @Guid" : "Id = @Id";
            var sql = $"UPDATE {typeof(T).Name} SET Deleted = 1, Updated = @Updated WHERE {guidOrId}";

            var parameters = new { commandSettings.Entity.Guid, commandSettings.Entity.Id, Updated = DateTime.UtcNow };
            var affected = await connection.ExecuteAsync(sql, parameters, transaction, commandType: GetCommand(commandSettings));
            Dispose();
            return affected > 0;
        }

        // Obtém uma entidade específica pelo SQL e parâmetros fornecidos
        public virtual async Task<T> GetAsync(CommandSettings<T> commandSettings)
        {
            var result = await connection.QueryFirstOrDefaultAsync<T>(commandSettings.Query, commandSettings.Parameters, transaction, commandType: GetCommand(commandSettings));
            Dispose();
            return result;
        }

        // Obtém uma lista de entidades com parâmetros opcionais para paginação
        public virtual async Task<List<T>> GetAllAsync(CommandSettings<T> commandSettings)
        {
            var result = await connection.QueryAsync<T>(commandSettings.Query, commandSettings.Parameters, transaction, commandType: GetCommand(commandSettings));
            Dispose();
            return result.ToList();
        }

        private CommandType GetCommand(CommandSettings<T> commandSettings)
        {
            var result = new CommandType();
            switch (commandSettings.CommandType)
            {
                case TypeCommand.Query:
                    result = CommandType.Text;
                    break;
                case TypeCommand.Procedure:
                    result = CommandType.Text;
                    break;
                default:
                    result = CommandType.Text;
                    break;
            }
            return result;
        }

        // Inicia uma transação se ainda não houver uma ativa
        public virtual async Task BeginTransactionAsync()
        {
            if (transaction != null && transaction.Connection != null)
                transaction = connection.BeginTransaction();
        }

        // Comita a transação aberta e marca como comitada
        public virtual async Task<bool> CommitAsync()
        {
            transaction.Commit();
            _committed = true;
            return await Task.FromResult(true);
        }

        public void Dispose()
        {
            if (transaction != null && transaction.Connection != null)
            {
                if (!_committed)
                    transaction.Rollback();

                transaction.Dispose();
            }

            connection.Close();
            connection.Dispose();
        }
    }
}
