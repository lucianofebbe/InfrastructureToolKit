using InfrastructureToolKit.Settings.AudioAndVideo.MediaStream.Settings;

namespace Interfaces.AudioAndVideo.MediaStream
{
    public interface IChat
    {
        Task SendMessageAsync(ChatSettings settings);
    }
}
