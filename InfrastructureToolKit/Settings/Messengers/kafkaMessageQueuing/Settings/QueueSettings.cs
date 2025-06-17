namespace InfrastructureToolKit.Settings.Messengers.kafkaMessageQueuing.Settings
{
    // Configurações relacionadas à fila do Kafka, como o tópico a ser usado
    public record QueueSettings
    {
        // Nome do tópico Kafka para envio e recebimento de mensagens
        public string Topic { get; set; }
    }
}
