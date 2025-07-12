using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Settings.DataBases.AdoNet.Settings;
using System.Data;

namespace InfrastructureToolKit.Interfaces.DataBase.AdoNet.Ado
{
    // Interface para operações ADO.NET assíncronas com suporte a transações
    public interface IAdo<T> : IAsyncDisposable where T : BaseEntitiesSql
    {
        // Executa uma consulta e retorna os dados em um DataTable
        Task<DataTable> ExecuteQueryAsync(CommandSettings commandSettings);

        // Executa um comando que não retorna resultado (ex: INSERT, UPDATE, DELETE)
        Task<Object> ExecuteNonQueryAsync(CommandSettings commandSettings);

        //Converte DataTable para lista, mas somente tipos simples, cuidado com os nomes dos tipos, tem que ser identicos
        //tipos complexos, utilize uma implementacao propria.
        Task<List<T>> ConverterDataTableToListT(DataTable table);

        // Inicia uma transação no contexto atual
        Task BeginTransactionAsync();

        // Marca a transação como concluída com sucesso para posterior commit
        void MarkCommit();
    }
}
