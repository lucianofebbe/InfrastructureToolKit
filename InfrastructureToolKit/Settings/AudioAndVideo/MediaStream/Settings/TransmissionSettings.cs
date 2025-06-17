namespace InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings
{
    public record TransmissionSettings
    {
        public string RoomName { get; set; } = string.Empty;
        public string TransmitterName { get; set; } = string.Empty;
        public bool EnableRecording { get; set; } = false;
        public string? RecordingPath { get; set; }
    }
}
