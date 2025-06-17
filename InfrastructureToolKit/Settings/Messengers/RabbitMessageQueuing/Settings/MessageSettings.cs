namespace InfrastructureToolKit.Settings.Messengers.RabbitMessageQueuing.Settings
{
    // Representa as configurações relacionadas às mensagens trafegadas no RabbitMQ
    public record MessageSettings
    {
        // Mensagem a ser enviada
        public string Message { get; set; }

        // Resultado ou resposta da mensagem recebida
        public string MessageResult { get; set; }
        public CancellationToken CancellationToken { get; set; }

        // Delegado assíncrono executado quando uma mensagem é recebida
        public Func<string, Task<string>>? OnMessageReceivedAsync { get; set; }
    }
}
