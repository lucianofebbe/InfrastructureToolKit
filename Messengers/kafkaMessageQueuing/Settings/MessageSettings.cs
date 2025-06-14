namespace InfrastructureToolKit.Messengers.kafkaMessageQueuing.Settings
{
    // Configurações relacionadas à mensagem a ser enviada ou recebida pelo Kafka
    public record MessageSettings
    {
        // Conteúdo da mensagem que será enviada ou processada
        public string Message { get; set; }

        // Status de entrega da mensagem (true se entregue com sucesso, false caso contrário)
        public bool DeliveryStatus { get; set; }
        public CancellationToken CancellationToken { get; set; }

        // Callback assíncrono chamado quando uma mensagem é recebida, recebe a mensagem como string e retorna uma string (opcional)
        public Func<string, Task<string>>? OnMessageReceivedAsync { get; set; }
    }
}
