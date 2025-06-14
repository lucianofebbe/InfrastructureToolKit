namespace InfrastructureToolKit.Messengers.RabbitMessageQueuing.Settings
{
    // Representa as configurações do servidor e do comportamento de troca de mensagens no RabbitMQ
    public record ServerSettings
    {
        // Nome do exchange utilizado para roteamento das mensagens
        public string Exchange { get; set; }

        // Tipo do exchange (ex: direct, fanout, topic, etc.)
        public string ExchangeType { get; set; } = "direct";

        // Indica se a fila deve ser criada automaticamente
        public bool CreateQueue { get; set; }

        // Indica se a fila é exclusiva para a conexão atual
        public bool Exclusive { get; set; }

        // Indica se a fila deve sobreviver a reinícios do servidor RabbitMQ
        public bool Durable { get; set; }

        // Indica se a fila deve ser automaticamente deletada quando não estiver em uso
        public bool AutoDelete { get; set; }

        // Indica se a publicação da mensagem é obrigatória (requer que a fila exista)
        public bool Mandatory { get; set; } = true;

        // Indica se múltiplas mensagens podem ser confirmadas com uma única resposta (confirmação em lote)
        public bool Multiple { get; set; }

        // Indica se o reconhecimento da mensagem (ack) é automático
        public bool AutoAck { get; set; } = true;
    }
}
