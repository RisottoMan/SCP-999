using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;
public class YippeeAbility : IAbility
{
    public string Name { get; } = "Yippee";
    public string Description { get; } = "Play the Yippee sound";
    public int KeyId { get; } = 9990;
    public int Cooldown { get; } = 3;
    public void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += KeybindActivateAbility;
    }

    public void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= KeybindActivateAbility;
    }
    
    private void KeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != this.KeyId || !keybindSetting.SyncIsPressed)
            return;
        
        if (!Player.TryGet(referenceHub, out Player player))
            return;

        AudioPlayer audioPlayer = player.GameObject.GetComponent<PlayerComponent>().GetCurrentAudioPlayer;
        if (audioPlayer is null)
            return;

        audioPlayer.AddClip($"yippee-tbh{Random.Range(0, 2)}");
        Log.Debug("[HealAbility] Activating the Yippee ability");
    }
}