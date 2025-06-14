namespace InfrastructureToolKit.Cryptographies.PBKDF2Cryptography.Settings
{
    public record PBKDF2CryptographyCreateHashResultSettings
    {
        public string Hash { get; set; }
        public string? Salt { get; set; }
    }
}
