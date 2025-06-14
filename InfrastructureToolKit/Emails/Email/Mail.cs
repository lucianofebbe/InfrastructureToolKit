using InfrastructureToolKit.Emails.Settings;
using InfrastructureToolKit.Interfaces.Emails.Email;
using System.Net;
using System.Net.Mail;

namespace InfrastructureToolKit.Emails.Email
{
    public class Mail : IMails
    {
        private SmtpSettings SmtpSettings;
        public Mail(SmtpSettings SmtpSettings)
        {
            this.SmtpSettings = SmtpSettings;
        }

        // Método assíncrono para envio do email com as configurações definidas
        public virtual async Task SendMail(MailsSettings MailsSettings)
        {
            // Cria o objeto MailMessage que representa o email a ser enviado
            using var email = new MailMessage();

            // Define o remetente do email
            email.From = new MailAddress(SmtpSettings.Email);

            // Define o destinatário principal do email
            email.To.Add(new MailAddress(MailsSettings.ToEmail));

            // Define o assunto do email
            email.Subject = MailsSettings.Subject;

            // Adiciona os destinatários em cópia (CC), caso existam
            if (MailsSettings.CcEmail != null)
            {
                foreach (var item in MailsSettings.CcEmail)
                {
                    if (!string.IsNullOrEmpty(item.ToEmail))
                        email.CC.Add(new MailAddress(item.ToEmail));
                }
            }

            // Adiciona anexos ao email, caso existam
            if (MailsSettings.Attachments != null)
            {
                foreach (var item in MailsSettings.Attachments)
                {
                    if (!string.IsNullOrEmpty(item.FileName))
                    {
                        // Cria attachment a partir do conteúdo em memória e nome do arquivo
                        var attachment = new Attachment(new MemoryStream(item.FileBytes), item.FileName);
                        email.Attachments.Add(attachment);
                    }
                }
            }

            // Define o corpo do email e se é HTML ou texto simples
            email.Body = MailsSettings.Body;
            email.IsBodyHtml = MailsSettings.BodyHtml;

            // Configura e cria o cliente SMTP para envio do email
            using var smtp = new SmtpClient(SmtpSettings.Host, SmtpSettings.Port)
            {
                EnableSsl = SmtpSettings.EnableSsl,
                UseDefaultCredentials = SmtpSettings.UseDefaultCredentials,
                Credentials = new NetworkCredential(SmtpSettings.Email, SmtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            // Envia o email de forma assíncrona
            await smtp.SendMailAsync(email);
        }
    }
}
