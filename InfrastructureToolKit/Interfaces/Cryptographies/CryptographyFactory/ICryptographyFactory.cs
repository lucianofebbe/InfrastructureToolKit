using InfrastructureToolKit.Interfaces.Cryptographies.AesCryptography;
using InfrastructureToolKit.Interfaces.Cryptographies.JwtCryptography;
using InfrastructureToolKit.Interfaces.Cryptographies.PBKDF2Cryptography;

namespace InfrastructureToolKit.Interfaces.Cryptographies.CryptographyFactory
{
    // Interface responsável pela criação de instâncias de criptografia específicas
    public interface ICryptographyFactory
    {
        // Cria uma instância de criptografia AES usando a chave fornecida
        Task<IAesCryptography> CreateAesCryptography(string key);

        // Cria uma instância de criptografia baseada em PBKDF2
        Task<IPBKDF2Cryptography> CreatePBKDF2Cryptography();

        // Cria uma instância para geração e validação de tokens JWT
        Task<IJwtCryptography> CreateJwtCryptography();
    }
}
