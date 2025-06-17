namespace InfrastructureToolKit.Settings.Images.Settings
{
    public record ImagesParametersSettings
    {
        public System.Drawing.Image Image { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
