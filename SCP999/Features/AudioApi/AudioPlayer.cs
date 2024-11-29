using Mirror;
using SCPSLAudioApi.AudioCore;
using System;
using System.IO;
using Object = UnityEngine.Object;
using VoiceChat;
using Exiled.API.Features;

namespace SCP999;
public static class AudioPlayer
{
    public static ReferenceHub AudioBot = new ReferenceHub();

    public static AudioPlayerBase PlayAudio(string audioFile, byte volume, bool loop)
    {
        if (AudioBot == null) AudioBot = AddDummy();

        StopAudio();

        var path = Path.Combine(Plugin.Singleton.Config.AudioPath, audioFile);

        AudioPlayerBase audioPlayer = AudioPlayerBase.Get(AudioBot);
        audioPlayer.Enqueue(path, -1);
        audioPlayer.LogDebug = false;
        audioPlayer.BroadcastChannel = VoiceChatChannel.Intercom;
        audioPlayer.Volume = volume * (Plugin.Singleton.Config.Volume / 100f);
        audioPlayer.Loop = loop;
        audioPlayer.Play(0);

        return audioPlayer;
    }
    public static void StopAudio()
    {
        if (AudioBot == null) return;

        var audioPlayer = AudioPlayerBase.Get(AudioBot);

        if (audioPlayer.CurrentPlay != null)
        {
            audioPlayer.Stoptrack(true);
            audioPlayer.OnDestroy();
        }
    }

    public static void PlayPlayerAudio(Player player, string audioFile, byte volume)
    {
        var path = Path.Combine(Plugin.Singleton.Config.AudioPath, audioFile);

        var audioPlayer = AudioPlayerBase.Get(player.ReferenceHub);
        audioPlayer.Enqueue(path, -1);
        audioPlayer.LogDebug = false;
        audioPlayer.BroadcastChannel = VoiceChatChannel.Proximity;
        audioPlayer.Volume = volume * (Plugin.Singleton.Config.Volume / 100f);
        audioPlayer.Loop = false;
        audioPlayer.Play(0);
    }

    public static ReferenceHub AddDummy()
    {
        var newPlayer = Object.Instantiate(NetworkManager.singleton.playerPrefab);
        var fakeConnection = new FakeConnection(0);
        var hubPlayer = newPlayer.GetComponent<ReferenceHub>();
        NetworkServer.AddPlayerForConnection(fakeConnection, newPlayer);
        hubPlayer.authManager.InstanceMode = CentralAuth.ClientInstanceMode.Unverified;
        
        // CharacterClassManager.instance
        try
        {
            hubPlayer.nicknameSync.SetNick("SCP-999");
        }
        catch (Exception) { }

        return hubPlayer;
    }

    public static void RemoveDummy()
    {
        var audioPlayer = AudioPlayerBase.Get(AudioBot);

        if (audioPlayer.CurrentPlay != null)
        {
            audioPlayer.Stoptrack(true);
            audioPlayer.OnDestroy();
        }

        AudioBot.OnDestroy();
        CustomNetworkManager.TypedSingleton.OnServerDisconnect(AudioBot.connectionToClient);
        Object.Destroy(AudioBot.gameObject);
    }
}