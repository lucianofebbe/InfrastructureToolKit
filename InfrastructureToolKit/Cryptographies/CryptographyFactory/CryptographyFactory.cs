using InfrastructureToolKit.Cryptographies.JwtCryptography;
using InfrastructureToolKit.Interfaces.Cryptographies.AesCryptography;
using InfrastructureToolKit.Interfaces.Cryptographies.CryptographyFactory;
using InfrastructureToolKit.Interfaces.Cryptographies.JwtCryptography;
using InfrastructureToolKit.Interfaces.Cryptographies.PBKDF2Cryptography;

namespace InfrastructureToolKit.Cryptographies.CryptographyFactory
{
    /// <summary>
    /// Fábrica de criptografia para criar instâncias das diferentes implementações de criptografia usadas no sistema.
    /// </summary>
    public class CryptographyFactory : ICryptographyFactory
    {
        /// <summary>
        /// Cria uma instância de criptografia AES utilizando a chave fornecida.
        /// </summary>
        /// <param name="key">Chave para a criptografia AES.</param>
        /// <returns>Instância de IAesCryptography configurada com a chave.</returns>
        public virtual async Task<IAesCryptography> CreateAesCryptography(string key)
        {
            IAesCryptography cripto = new AesCryptography.AesCryptography(key);
            return cripto;
        }

        /// <summary>
        /// Cria uma instância da criptografia PBKDF2 para derivação de chaves.
        /// </summary>
        /// <returns>Instância de IPBKDF2Cryptography.</returns>
        public virtual async Task<IPBKDF2Cryptography> CreatePBKDF2Cryptography()
        {
            IPBKDF2Cryptography cripto = new PBKDF2Cryptography.PBKDF2Cryptography();
            return cripto;
        }

        /// <summary>
        /// Cria uma instância da criptografia JWT para geração e validação de tokens.
        /// </summary>
        /// <returns>Instância de IJwtCryptography.</returns>
        public virtual async Task<IJwtCryptography> CreateJwtCryptography()
        {
            IJwtCryptography cripto = new JWtCryptography();
            return cripto;
        }
    }
}
