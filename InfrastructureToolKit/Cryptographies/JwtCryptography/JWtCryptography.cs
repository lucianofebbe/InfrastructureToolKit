using InfrastructureToolKit.Interfaces.Cryptographies.JwtCryptography;
using InfrastructureToolKit.Settings.Cryptographies.JwtCryptography.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfrastructureToolKit.Cryptographies.JwtCryptography
{
    /// <summary>
    /// Implementação da criptografia JWT para geração de tokens JSON Web Token.
    /// </summary>
    public class JWtCryptography : IJwtCryptography
    {
        /// <summary>
        /// Gera um token JWT baseado nas configurações passadas.
        /// </summary>
        /// <param name="settings">Configurações para geração do token.</param>
        /// <returns>Token JWT como string.</returns>
        public virtual async Task<JwtCryptographyResultSettings> GenerateJwtAsync(JwtCryptographyCreateSettings settings)
        {
            var result = new JwtCryptographyResultSettings() { Result = await GenerateToken(settings) };
            return result;
        }

        /// <summary>
        /// Método interno que realiza a criação do token JWT.
        /// </summary>
        /// <param name="settings">Configurações para geração do token.</param>
        /// <returns>Token JWT como string.</returns>
        private async Task<string> GenerateToken(JwtCryptographyCreateSettings settings)
        {
            var issuer = settings.AuthIssuer;       // Emissor do token
            var audience = settings.AuthAudience;   // Público-alvo do token
            var secret = settings.AuthSecret;       // Chave secreta para assinatura
            var claims = new List<Claim>()
            {
                new("IdUsuario", settings.Value)    // Reivindicação customizada com Id do usuário
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            return await Task.FromResult(
                tokenHandler.WriteToken(
                    new JwtSecurityToken(
                        issuer: issuer,
                        audience: audience,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(120)), // Token expira em 120 minutos
                        signingCredentials: await GetSigningCredentials(secret) // Credenciais de assinatura
                    )));
        }

        /// <summary>
        /// Obtém as credenciais de assinatura para o token usando a chave secreta.
        /// </summary>
        /// <param name="segredo">Chave secreta para assinatura do token.</param>
        /// <returns>Credenciais de assinatura.</returns>
        private async Task<SigningCredentials> GetSigningCredentials(string segredo)
        {
            return await Task.FromResult(new SigningCredentials(
                GetSymmetricKey(segredo),
                SecurityAlgorithms.HmacSha256)); // Algoritmo de assinatura HMAC SHA256
        }

        /// <summary>
        /// Cria a chave simétrica a partir da chave secreta em string.
        /// </summary>
        /// <param name="segredo">Chave secreta.</param>
        /// <returns>Objeto SymmetricSecurityKey.</returns>
        private SymmetricSecurityKey GetSymmetricKey(string segredo) =>
            new(Encoding.UTF8.GetBytes(segredo));
    }
}
