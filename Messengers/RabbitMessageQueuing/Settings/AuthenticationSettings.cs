namespace InfrastructureToolKit.Messengers.RabbitMessageQueuing.Settings
{
    // Representa as configurações de autenticação para conexão com o RabbitMQ
    public record AuthenticationSettings
    {
        // Nome do host do servidor RabbitMQ
        public string HostName { get; set; }

        // Nome de usuário para autenticação
        public string UserName { get; set; }

        // Senha correspondente ao nome de usuário
        public string Password { get; set; }

        // Porta utilizada para a conexão com o servidor RabbitMQ
        public int Port { get; set; }
    }
}
