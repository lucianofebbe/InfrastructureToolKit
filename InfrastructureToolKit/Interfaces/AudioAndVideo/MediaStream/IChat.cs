using InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings;

namespace InfrastructureToolKit.Interfaces.AudioAndVideo.MediaStream
{
    public interface IChat
    {
        Task SendMessageAsync(ChatSettings settings);
    }
}
