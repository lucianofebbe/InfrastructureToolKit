using InfrastructureToolKit.Settings.Cryptographies.PBKDF2Cryptography.Settings;

namespace InfrastructureToolKit.Interfaces.Cryptographies.PBKDF2Cryptography
{
    // Interface responsável por operações de hash utilizando o algoritmo PBKDF2
    public interface IPBKDF2Cryptography
    {
        // Compara uma senha com um hash e salt fornecidos
        Task<PBKDF2CryptographyCompareHashResultSettings> CompareHashAsync(PBKDF2CryptographyCompareHashSettings settings);

        // Gera um hash e um novo salt para uma senha fornecida
        Task<PBKDF2CryptographyCreateHashResultSettings> CreateHashAsync(PBKDF2CryptographyCreateHashSettings settings);
    }
}
