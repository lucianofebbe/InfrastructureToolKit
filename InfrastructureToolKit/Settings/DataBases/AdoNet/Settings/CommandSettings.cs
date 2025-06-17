using System.Data.Common;


namespace InfrastructureToolKit.Settings.DataBases.AdoNet.Settings
{
    public record CommandSettings
    {
        // Comando SQL ou nome da stored procedure a ser executada
        public string Query { get; set; }

        // Parâmetros que serão passados para o comando (query ou procedure)
        public List<DbParameter> Parameters { get; set; }

        // Tipo do comando: Query SQL ou Stored Procedure
        public TypeCommand CommandType { get; set; }

        // Indica se o comando deve executar ExecuteScalar (retorna um valor)
        public bool ExecuteScalar { get; set; }
        public int CommandTimeout { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }

    // Enum para definir o tipo de comando que será executado
    public enum TypeCommand
    {
        Query,      // Comando SQL padrão
        Procedure   // Stored Procedure
    }
}