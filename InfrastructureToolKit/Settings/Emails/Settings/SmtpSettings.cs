namespace InfrastructureToolKit.Settings.Emails.Settings
{
    // Representa as configurações do servidor SMTP para envio de emails
    public record SmtpSettings
    {
        // Endereço do servidor SMTP
        public string Host { get; set; }

        // Porta do servidor SMTP
        public int Port { get; set; }

        // Email usado como remetente/autenticação no servidor SMTP
        public string Email { get; set; }

        // Senha para autenticação no servidor SMTP
        public string Password { get; set; }

        // Indica se o SSL deve ser habilitado na conexão SMTP
        public bool EnableSsl { get; set; }

        // Indica se devem ser usadas as credenciais padrão do sistema
        public bool UseDefaultCredentials { get; set; }
    }
}
