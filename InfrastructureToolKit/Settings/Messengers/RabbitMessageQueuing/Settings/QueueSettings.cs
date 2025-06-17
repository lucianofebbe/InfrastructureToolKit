namespace InfrastructureToolKit.Settings.Messengers.RabbitMessageQueuing.Settings
{
    // Representa as configurações da fila utilizada no RabbitMQ
    public record QueueSettings
    {
        // Chave de roteamento utilizada para envio da mensagem
        public string RoutingKey { get; set; }

        // Nome da fila principal para envio/recebimento de mensagens
        public string QueueName { get; set; }

        // Nome da fila utilizada para receber respostas (reply)
        public string QueueNameReply { get; set; }

        // Argumentos adicionais utilizados na declaração da fila
        public Dictionary<string, object?>? Arguments { get; set; }
    }
}
