using Exiled.API.Features;
using Scp999.Interfaces;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;
public class SizeAbility : IAbility
{
    public string Name { get; } = "Size";
    public string Description { get; } = "Change the size of a specific player";
    public int KeyId { get; } = 9991;
    public int Cooldown { get; } = 30;
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

        //Raycast
        //ChangeSize
        
        Log.Debug("[HealAbility] Activating the size ability");
    }
}