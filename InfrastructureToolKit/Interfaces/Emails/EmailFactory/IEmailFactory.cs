using InfrastructureToolKit.Interfaces.Emails.Email;
using InfrastructureToolKit.Settings.Emails.Settings;

namespace InfrastructureToolKit.Interfaces.Emails.EmailFactory
{
    // Interface que define a fábrica para criação de objetos de e-mail (IMails).
    public interface IEmailFactory
    {
        // Cria um objeto IMails utilizando configurações específicas fornecidas.
        Task<IMails> Create(SmtpSettings SmtpSettings);
    }
}
