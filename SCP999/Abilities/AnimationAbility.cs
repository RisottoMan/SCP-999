using Exiled.API.Features;
using Scp999.Interfaces;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;
public class AnimationAbility : IAbility
{
    public string Name { get; } = "Random Animation";
    public string Description { get; } = "Play a random funny animation";
    public int KeyId { get; } = 9994;
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

        //var schematic = player.GameObject.GetComponent<PlayerComponent>().GetCurrentSchematic;
        //Log.Error(schematic);
        
        //PlayAnimation();
        
        Log.Debug("[HealAbility] Activating the random animation ability");
    }
}