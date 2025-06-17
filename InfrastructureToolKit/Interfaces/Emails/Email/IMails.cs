using InfrastructureToolKit.Settings.Emails.Settings;

namespace InfrastructureToolKit.Interfaces.Emails.Email
{
    // Interface que define o contrato para envio de e-mails.
    public interface IMails
    {
        // Método assíncrono para envio do e-mail.
        Task SendMail(MailsSettings MailsSettings);
    }
}
