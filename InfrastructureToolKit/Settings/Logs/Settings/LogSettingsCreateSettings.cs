namespace InfrastructureToolKit.Settings.Logs.Settings
{
    // Registro que representa os dados básicos de um log
    public record LogSettingsCreateSettings
    {
        // Data e hora do evento registrado no log (em formato string)
        public DateTime Data { get; set; }
        public Exception Exception { get; set; }
    }
}
