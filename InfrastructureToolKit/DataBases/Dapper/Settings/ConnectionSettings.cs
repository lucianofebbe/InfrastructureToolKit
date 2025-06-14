using System.Data;

namespace InfrastructureToolKit.DataBase.Dapper.Settings
{
    public record ConnectionSettings
    {
        public IDbConnection Connection { get; set; }
        public bool EnableTransaction { get; set; }
    }
}
