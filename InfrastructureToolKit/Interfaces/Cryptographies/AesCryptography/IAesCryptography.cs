using InfrastructureToolKit.Settings.Cryptographies.AesCryptography.Settings;

namespace InfrastructureToolKit.Interfaces.Cryptographies.AesCryptography
{
    // Interface responsável por operações de criptografia e descriptografia usando AES
    public interface IAesCryptography
    {
        // Criptografa um texto simples e retorna um array de bytes criptografado
        Task<AesCryptographyResultSettings> EncryptAsync(AesCryptographyCreateSettings settings);

        // Descriptografa um array de bytes e retorna o texto original
        Task<AesCryptographyResultSettings> DecryptAsync(AesCryptographyCreateSettings settings);
    }
}
