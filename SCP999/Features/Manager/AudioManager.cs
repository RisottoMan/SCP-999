using System.Collections.Generic;
using System.IO;
using Exiled.API.Features;
using UnityEngine;

namespace Scp999.Features.Manager;
public class AudioManager
{
    private static List<string> _audioNameList = new()
    {
        "yippee-tbh1",
        "yippee-tbh2"
    };
    
    public static void AddAudioPlayer(Player player, out AudioPlayer audioPlayer)
    {
        if (player is null)
        {
            Log.Error($"[AddAudioPlayer] The audioPlayer is null");
            audioPlayer = null;
            return;
        }
        
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

        LoadAudioFiles();
    }

    public static void RemoveAudioPlayer(AudioPlayer audioPlayer)
    {
        audioPlayer.RemoveAllClips();
        audioPlayer.Destroy();
    }

    private static void LoadAudioFiles()
    {
        string path = Plugin.Singleton.AudioPath;
        
        foreach (string audioName in _audioNameList)
        {
            if (!AudioClipStorage.AudioClips.ContainsKey(audioName))
            {
                string filePath = Path.Combine(path, audioName) + ".ogg";
            
                if (!AudioClipStorage.LoadClip(filePath, audioName))
                {
                    Log.Error($"[AddAudioPlayer] The audio file {audioName} was not found for playback");
                }
            }
        }
    }
}