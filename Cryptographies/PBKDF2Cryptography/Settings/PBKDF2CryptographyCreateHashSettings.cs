namespace InfrastructureToolKit.Cryptographies.PBKDF2Cryptography.Settings
{
    public record PBKDF2CryptographyCreateHashSettings
    {
        public string Senha { get; set; }
        public string Hash { get; set; }
        public string? Salt { get; set; }
    }
}
