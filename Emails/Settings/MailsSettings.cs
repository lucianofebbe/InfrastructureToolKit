namespace InfrastructureToolKit.Emails.Settings
{
    // Representa as configurações principais do email a ser enviado
    public record class MailsSettings
    {
        // Destinatário principal do email
        public string ToEmail { get; set; }

        // Assunto do email
        public string Subject { get; set; }

        // Corpo do email (texto ou HTML)
        public string Body { get; set; }

        // Indica se o corpo do email está em formato HTML
        public bool BodyHtml { get; set; }

        // Lista de destinatários em cópia (Cc)
        public List<CcMailSettings> CcEmail { get; set; }

        // Lista de anexos que serão enviados junto ao email
        public List<EmailAttachmentsSettings> Attachments { get; set; }
    }

    public record EmailAttachmentsSettings
    {
        // Nome do arquivo do anexo
        public string FileName { get; set; }

        // Conteúdo do arquivo em bytes
        public byte[] FileBytes { get; set; }

        // Tipo de conteúdo (MIME) do arquivo, ex: "application/pdf"
        public string ContentType { get; set; }
    }

    public record CcMailSettings
    {
        // Endereço de email que receberá a cópia
        public string ToEmail { get; set; }
    }
}
