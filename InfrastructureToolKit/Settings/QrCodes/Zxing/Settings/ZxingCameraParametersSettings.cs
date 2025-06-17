namespace InfrastructureToolKit.Settings.QrCodes.Zxing.Settings
{
    public record ZxingCameraParametersSettings
    {

        public int CameraIndex { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
