namespace InfrastructureToolKit.Settings.Messengers.kafkaMessageQueuing.Settings
{
    // Configurações para autenticação no Kafka, incluindo servidor, grupo, offset, usuário e senha, além dos protocolos de segurança
    public record AuthenticationSettings
    {
        // Endereço do servidor Kafka (bootstrap servers)
        public string Server { get; set; }

        // Identificador do grupo de consumidores Kafka
        public string GroupId { get; set; }

        // Política de offset automático (ex: earliest, latest)
        public string AutoOffsetReset { get; set; }

        // Nome de usuário para autenticação SASL
        public string Username { get; set; }

        // Senha para autenticação SASL
        public string Password { get; set; }

        // Protocolo de segurança utilizado na conexão (ex: SASL_SSL)
        public Protocol Protocol { get; set; }

        // Mecanismo SASL para autenticação (ex: PLAIN, SCRAM-SHA-256)
        public Sasl Sasl { get; set; }
    }

    public enum Sasl
    {
        Gssapi,        // Autenticação Kerberos
        Plain,         // Autenticação simples com usuário e senha
        ScramSha256,   // Autenticação SCRAM usando SHA-256
        ScramSha512,   // Autenticação SCRAM usando SHA-512
        OAuthBearer    // Autenticação via token OAuth
    }

    public enum Protocol
    {
        // Comunicação sem criptografia
        Plaintext,

        // Comunicação com SSL (criptografia)
        Ssl,

        // Comunicação SASL sem criptografia
        SaslPlaintext,

        // Comunicação SASL com SSL (criptografia)
        SaslSsl
    }
}
