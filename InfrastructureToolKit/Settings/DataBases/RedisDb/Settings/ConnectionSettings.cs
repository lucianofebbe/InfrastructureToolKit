namespace InfrastructureToolKit.Settings.DataBases.RedisDb.Settings
{
    public record ConnectionSettings
    {
        public List<string> EndPoints { get; set; }
        public string Password { get; set; }
        public bool AbortOnConnectFail { get; set; }
    }
}
