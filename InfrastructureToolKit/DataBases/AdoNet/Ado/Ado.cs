using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.DataBase.AdoNet.Ado;
using InfrastructureToolKit.Settings.DataBases.AdoNet.Settings;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace InfrastructureToolKit.DataBases.AdoNet.Ado
{
    public class Ado<T> : IAdo<T> where T : BaseEntitiesSql
    {
        private DbProviderFactory provider;
        private DbTransaction transaction;
        private DbConnection connection;
        private bool committed;

        // Construtor: recebe configurações e inicializa conexão usando provider genérico (ex: SqlClient, Npgsql)
        public Ado(ConnectionSettings connectionSettings)
        {
            provider = DbProviderFactories.GetFactory(connectionSettings.ProviderName);
            connection = provider.CreateConnection();
            connection.ConnectionString = connectionSettings.ConnectionString;
        }

        // Inicia transação, lançando exceção se já houver uma em andamento
        public virtual async Task BeginTransactionAsync()
        {
            if (transaction != null)
                throw new InvalidOperationException("Transação já foi iniciada.");

            transaction = connection.BeginTransaction();
        }

        // Executa consulta e retorna resultado em DataTable
        public virtual async Task<DataTable> ExecuteQueryAsync(CommandSettings commandSettings)
        {
            await connection.OpenAsync(commandSettings.CancellationToken);

            await using var command = connection.CreateCommand();
            command.CommandText = commandSettings.Query;

            // Define se é procedure ou query simples
            switch (commandSettings.CommandType)
            {
                case TypeCommand.Procedure:
                    command.CommandType = CommandType.StoredProcedure;
                    break;
                case TypeCommand.Query:
                    command.CommandType = CommandType.Text;
                    break;
            }

            if (commandSettings.Parameters.Count > 0)
                command.Parameters.AddRange(commandSettings.Parameters.ToArray());

            using var reader = await command.ExecuteReaderAsync(commandSettings.CancellationToken);

            var table = new DataTable();
            table.Load(reader);
            return table;
        }

        // Executa comando não query (insert, update, delete), suporta ExecuteScalar para retornar valor
        public virtual async Task<int> ExecuteNonQueryAsync(CommandSettings commandSettings)
        {
            await connection.OpenAsync(commandSettings.CancellationToken);

            int result;
            await using var command = connection.CreateCommand();
            command.CommandText = commandSettings.Query;
            command.Transaction = transaction;

            switch (commandSettings.CommandType)
            {
                case TypeCommand.Procedure:
                    command.CommandType = CommandType.StoredProcedure;
                    break;
                case TypeCommand.Query:
                    command.CommandType = CommandType.Text;
                    break;
            }

            if (commandSettings.Parameters.Count > 0)
                command.Parameters.AddRange(commandSettings.Parameters.ToArray());

            if (commandSettings.ExecuteScalar)
            {
                var scalarResult = await command.ExecuteScalarAsync(commandSettings.CancellationToken);
                result = Convert.ToInt32(scalarResult);
            }
            else
                result = await command.ExecuteNonQueryAsync(commandSettings.CancellationToken);

            return result;
        }
        
        //Converter DataTable para lista, mas somente tipos simples, cuidado com os nomes dos tipos, tem que ser identicos
        //tipos complexos, utilize uma implementacao propria.
        public virtual async Task<List<T>> ConverterDataTableToListT(DataTable table)
        {
            var list = new List<T>();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (DataRow row in table.Rows)
            {
                var item = (T)Activator.CreateInstance(typeof(T))!;

                foreach (var prop in properties)
                {
                    if (!table.Columns.Contains(prop.Name) || row[prop.Name] == DBNull.Value)
                        continue;

                    var propertyType = prop.PropertyType;
                    var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

                    object safeValue = Convert.ChangeType(row[prop.Name], targetType);
                    prop.SetValue(item, safeValue);
                }

                list.Add(item);
            }

            return list;
        }

        // Marca que a transação deve ser comitada (commit)
        public virtual void MarkCommit()
        {
            committed = true;
        }

        // Dispose async para fechar conexão e transação, realizando commit ou rollback conforme flag
        public virtual async ValueTask DisposeAsync()
        {
            if (transaction == null || connection == null)
                return;

            try
            {
                if (committed)
                    await transaction.CommitAsync();
                else
                    await transaction.RollbackAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }
    }
}
