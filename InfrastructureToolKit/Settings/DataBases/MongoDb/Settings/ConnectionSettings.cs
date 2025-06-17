namespace InfrastructureToolKit.Settings.DataBases.MongoDb.Settings
{
    // Registro que encapsula as configurações necessárias para conexão com o MongoDB
    public record ConnectionSettings
    {
        // String de conexão com o servidor MongoDB
        public string ConnectionString { get; set; }

        // Nome do banco de dados MongoDB
        public string Database { get; set; }

        // Nome da coleção dentro do banco de dados
        public string Collection { get; set; }
    }
}
