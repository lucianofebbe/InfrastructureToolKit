using InfrastructureToolKit.Bases.Entities;

namespace InfrastructureToolKit.DataBase.Dapper.Settings
{
    public record CommandSettings<T> where T : BaseEntitiesSql
    {
        public T Entity { get; set; }
        public string Query { get; set; }
        public object Parameters { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool ExecuteScalar { get; set; }
        // Tipo do comando: Query SQL ou Stored Procedure
        public TypeCommand CommandType { get; set; }

    }

    // Enum para definir o tipo de comando que será executado
    public enum TypeCommand
    {
        Query,      // Comando SQL padrão
        Procedure   // Stored Procedure
    }
}
