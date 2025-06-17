namespace InfrastructureToolKit.Settings.Schedulers.Quartz.Settings
{
    /// <summary>
    /// Para criação das tabelas de persistência de jobs, link abaixo
    /// Não esquecer de setar true na propriedade EnablePersistence e
    /// passar a string de conexão na propriedade ConnectionString
    /// https://github.com/quartznet/quartznet/tree/main/database/tables
    /// </summary>
    public record PersistenceSettings
    {
        // Nome da instância do scheduler
        public string InstanceName { get; set; } = "QuartzWithDb";

        // Tipo da store usada para persistência (AdoJobStore)
        public string Type { get; set; } = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";

        // Define se as propriedades devem ser usadas como fallback
        public bool UseProperties { get; set; } = true;

        // Nome do dataSource a ser utilizado
        public string DataSource { get; set; } = "default";

        // Prefixo das tabelas no banco de dados
        public string TablePrefix { get; set; } = "QRTZ_";

        // String de conexão com o banco de dados
        public string ConnectionString { get; set; } = "Data Source=meu.db;Version=3;";

        // Provedor do banco de dados (ex: SQLServer, SQLite, etc.)
        public string Provider { get; set; } = "SQLite";

        // Tipo de serialização utilizada (ex: json, binary)
        public string SerializerType { get; set; } = "json";
    }
}
