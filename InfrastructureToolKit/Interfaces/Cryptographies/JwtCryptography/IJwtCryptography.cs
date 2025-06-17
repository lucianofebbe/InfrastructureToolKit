using InfrastructureToolKit.Settings.Cryptographies.JwtCryptography.Settings;

namespace InfrastructureToolKit.Interfaces.Cryptographies.JwtCryptography
{
    // Interface responsável por operações relacionadas à criptografia de tokens JWT
    public interface IJwtCryptography
    {
        // Gera um token JWT com base nas configurações fornecidas
        Task<JwtCryptographyResultSettings> GenerateJwtAsync(JwtCryptographyCreateSettings settings);
    }
}
