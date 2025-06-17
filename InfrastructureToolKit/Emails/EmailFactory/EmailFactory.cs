using InfrastructureToolKit.Emails.Email;
using InfrastructureToolKit.Interfaces.Emails.Email;
using InfrastructureToolKit.Interfaces.Emails.EmailFactory;
using InfrastructureToolKit.Settings.Emails.Settings;

namespace InfrastructureToolKit.Emails.EmailFactory
{
    public class EmailFactory : IEmailFactory
    {
        // Método assíncrono que cria uma instância de IMails usando configurações passadas como parâmetro
        public virtual async Task<IMails> Create(SmtpSettings SmtpSettings)
        {
            IMails mail = new Mail(SmtpSettings);
            return mail;
        }
    }
}
