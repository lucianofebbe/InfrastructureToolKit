namespace InfrastructureToolKit.Settings.Images.Settings
{
    public record ImagesSettings
    {
        public int TargetWidth { get; set; }
        public int TargetHeight { get; set; }
        public bool MaintainAspectRatio { get; set; } = true;
        public bool Fit { get; set; } = true;
        public float RotateDegrees { get; set; } = 0;
        public FlipMode Flip { get; set; } = FlipMode.None;
    }

    public enum FlipMode
    {
        None,
        Horizontal,
        Vertical,
        Both
    }
}
