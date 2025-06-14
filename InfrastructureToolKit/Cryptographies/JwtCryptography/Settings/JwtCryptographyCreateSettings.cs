namespace InfrastructureToolKit.Cryptographies.JwtCryptography.Settings
{
    /// <summary>
    /// Configurações para geração do token JWT.
    /// </summary>
    public record JwtCryptographyCreateSettings
    {
        /// <summary>
        /// Identifica o emissor do token (issuer).
        /// </summary>
        public string AuthIssuer { get; set; }

        /// <summary>
        /// Define o público-alvo do token (audience).
        /// </summary>
        public string AuthAudience { get; set; }

        /// <summary>
        /// Chave secreta usada para assinatura do token.
        /// </summary>
        public string AuthSecret { get; set; }

        /// <summary>
        /// Valor customizado que será incluído como claim no token (exemplo: Id do usuário).
        /// </summary>
        public string Value { get; set; }
    }
}
