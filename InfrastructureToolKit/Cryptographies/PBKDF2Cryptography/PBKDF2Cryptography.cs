using InfrastructureToolKit.Interfaces.Cryptographies.PBKDF2Cryptography;
using InfrastructureToolKit.Settings.Cryptographies.PBKDF2Cryptography.Settings;
using System.Security.Cryptography;

namespace InfrastructureToolKit.Cryptographies.PBKDF2Cryptography
{
    public class PBKDF2Cryptography : IPBKDF2Cryptography
    {
        /// <summary>
        /// Compara um hash existente com o hash gerado a partir da senha e salt fornecidos.
        /// </summary>
        /// <param name="senha">Senha em texto puro</param>
        /// <param name="hash">Hash base para comparação</param>
        /// <param name="salt">Salt usado para gerar o hash</param>
        /// <returns>True se os hashes coincidirem, caso contrário false</returns>
        public virtual async Task<PBKDF2CryptographyCompareHashResultSettings> CompareHashAsync(PBKDF2CryptographyCompareHashSettings settings)
        {
            var hashResult = await CreateHashAsync(new PBKDF2CryptographyCreateHashSettings()
            {
                Hash = settings.Hash,
                Salt = settings.Salt,
                Senha = settings.Senha
            });

            bool isMatch = string.Equals(hashResult.Hash, settings.Hash, StringComparison.Ordinal);
            var result = new PBKDF2CryptographyCompareHashResultSettings() { Result = isMatch };
            return result;
        }

        /// <summary>
        /// Cria um hash PBKDF2 para uma senha com um salt dado (ou novo salt se não fornecido).
        /// </summary>
        /// <param name="senha">Senha em texto puro</param>
        /// <param name="salt">Salt em Base64</param>
        /// <returns>Tupla com o hash em Base64 e o salt utilizado</returns>
        public virtual async Task<PBKDF2CryptographyCreateHashResultSettings> CreateHashAsync(PBKDF2CryptographyCreateHashSettings settings)
        {
            var result = new PBKDF2CryptographyCreateHashSettings()
            {
                Senha = settings.Senha,
                Salt = settings.Salt,
                Hash = settings.Hash
            };

            byte[] saltBytes = { };
            if (!string.IsNullOrEmpty(result.Salt))
                saltBytes = Convert.FromBase64String(result.Salt);  // Usa salt fornecido
            else
            {
                saltBytes = GenerateSalt();                   // Gera salt novo
                result.Salt = Convert.ToBase64String(saltBytes);
            }

            int iteracoes = 10000;          // Número de iterações PBKDF2
            int tamanhoHashEmBytes = 32;    // Tamanho do hash gerado

            byte[] hashBytes = CalculatePBKDF2(result.Senha, saltBytes, iteracoes, tamanhoHashEmBytes);

            string hashString = Convert.ToBase64String(hashBytes);

            result.Hash = hashString;
            return new PBKDF2CryptographyCreateHashResultSettings() { Hash = result.Hash, Salt = result.Salt };
        }

        /// <summary>
        /// Gera um salt aleatório de 16 bytes para uso em hashing.
        /// </summary>
        /// <returns>Array de bytes contendo o salt</returns>
        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Calcula o hash PBKDF2 usando senha, salt, número de iterações e tamanho do hash.
        /// </summary>
        /// <param name="senha">Senha em texto puro</param>
        /// <param name="salt">Salt em bytes</param>
        /// <param name="iteracoes">Número de iterações</param>
        /// <param name="tamanhoHashEmBytes">Tamanho do hash resultante</param>
        /// <returns>Array de bytes contendo o hash calculado</returns>
        private byte[] CalculatePBKDF2(string senha, byte[] salt, int iteracoes, int tamanhoHashEmBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, iteracoes))
            {
                return pbkdf2.GetBytes(tamanhoHashEmBytes);
            }
        }
    }
}
