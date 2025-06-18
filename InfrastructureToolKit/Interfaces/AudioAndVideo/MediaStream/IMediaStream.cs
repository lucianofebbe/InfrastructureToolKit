using InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings;

namespace InfrastructureToolKit.Interfaces.AudioAndVideo.MediaStream
{
    public interface IMediaStream
    {
        Task StartStreamAsync(MediaStreamSettings settings);
        Task StopStreamAsync(MediaStreamSettings settings);
        Task StartRecordingAsync(MediaStreamSettings settings);
        Task StopRecordingAsync(MediaStreamSettings settings);
    }
}
