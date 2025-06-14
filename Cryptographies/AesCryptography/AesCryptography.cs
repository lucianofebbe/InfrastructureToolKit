using InfrastructureToolKit.Cryptographies.AesCryptography.Settings;
using InfrastructureToolKit.Interfaces.Cryptographies.AesCryptography;
using System.Security.Cryptography;
using System.Text;

namespace InfrastructureToolKit.Cryptographies.AesCryptography
{
    /// <summary>
    /// Implementação da criptografia AES utilizando chave derivada de SHA256.
    /// Suporta criptografia e descriptografia em modo CBC com padding PKCS7.
    /// </summary>
    public class AesCryptography : IAesCryptography
    {
        private readonly byte[] key;

        /// <summary>
        /// Construtor que recebe a chave secreta em texto e gera um hash SHA256 para uso na criptografia.
        /// </summary>
        /// <param name="key">Chave secreta usada para criptografia.</param>
        public AesCryptography(string key)
        {
            using var sha256 = SHA256.Create();
            this.key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }

        /// <summary>
        /// Criptografa um texto simples (string) retornando o vetor de bytes contendo o IV concatenado com o texto cifrado.
        /// </summary>
        /// <param name="plainText">Texto simples a ser criptografado.</param>
        /// <returns>Array de bytes contendo o IV e o texto criptografado.</returns>
        public virtual async Task<AesCryptographyResultSettings> EncryptAsync(AesCryptographyCreateSettings settings)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.GenerateIV(); // Gera vetor de inicialização único para cada criptografia
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.Mode = CipherMode.CBC;

            var iv = aesAlg.IV;

            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);
            using var msEncrypt = new MemoryStream();

            // Escreve o vetor de inicialização (IV) no início do stream criptografado
            msEncrypt.Write(iv, 0, iv.Length);

            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(settings.Value); // Escreve o texto simples no stream de criptografia
            }

            return new AesCryptographyResultSettings()
            {
                Result = Convert.ToBase64String(msEncrypt.ToArray())
            };
        }

        /// <summary>
        /// Descriptografa o array de bytes contendo o IV concatenado com o texto cifrado e retorna o texto simples original.
        /// Base64
        /// </summary>
        /// <param name="cipherTextWithIv">Array de bytes contendo o IV (primeiros 16 bytes) seguido do texto cifrado.</param>
        /// <returns>Texto simples descriptografado.</returns>
        public virtual async Task<AesCryptographyResultSettings> DecryptAsync(AesCryptographyCreateSettings settings)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.Mode = CipherMode.CBC;

            var cipherTextWithIv = Convert.FromBase64String(settings.Value);
            // Extrai o vetor de inicialização (IV) dos primeiros 16 bytes do array
            var iv = cipherTextWithIv.Take(16).ToArray();
            // Extrai o texto cifrado a partir do byte 17 em diante
            var cipherText = cipherTextWithIv.Skip(16).ToArray();

            aesAlg.IV = iv;

            using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            // Retorna o texto descriptografado
            return new AesCryptographyResultSettings() { Result = await srDecrypt.ReadToEndAsync() };
        }
    }
}
