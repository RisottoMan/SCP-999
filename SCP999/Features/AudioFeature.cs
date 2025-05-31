using Exiled.API.Features;

namespace Scp999;
public class AudioFeature
{
    public static void AddAudioPlayer(Player player, out AudioPlayer audioPlayer)
    {
        audioPlayer = new AudioPlayer();
    }

    public static void RemoveAudioPlayer(Player player, AudioPlayer audioPlayer)
    {
    }
}