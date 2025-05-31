using Exiled.API.Features;
using UnityEngine;

namespace Scp999;
public class AudioFeature
{
    public static void AddAudioPlayer(Player player, out AudioPlayer audioPlayer)
    {
        audioPlayer = AudioPlayer.CreateOrGet($"Scp999 {player.Nickname}", onIntialCreation: (p) =>
        {        
            // Attach created audio player to player.
            p.transform.parent = player.GameObject.transform;

            // This created speaker will be in 3D space.
            Speaker speaker = p.AddSpeaker("Scp999-Main", isSpatial: true, minDistance: 5f, maxDistance: 15f);

            // Attach created speaker to player.
            speaker.transform.parent = player.GameObject.transform;

            // Set local position to zero to make sure that speaker is in player.
            speaker.transform.localPosition = Vector3.zero;
        });
    }

    public static void RemoveAudioPlayer(AudioPlayer audioPlayer)
    {
        audioPlayer.Destroy();
    }
}