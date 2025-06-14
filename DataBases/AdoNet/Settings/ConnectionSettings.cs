namespace InfrastructureToolKit.DataBase.AdoNet.Settings
{
    public record ConnectionSettings
    {
        // Nome do provedor de banco de dados, ex: "System.Data.SqlClient", "Npgsql" etc
        public string ProviderName { get; set; }

        // String de conexão que contém os parâmetros necessários para se conectar ao banco
        public string ConnectionString { get; set; }
        public bool EnableTransaction { get; set; }
    }
}