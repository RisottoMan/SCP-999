using Exiled.API.Features;
using Scp999.Interfaces;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;
public class HelloAbility : IAbility
{
    public string Name { get; } = "Hello";
    public string Description { get; } = "Greeting players or attracting attention";
    public int KeyId { get; } = 9992;
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

        //PlayAnimation();
        
        Log.Debug("[HealAbility] Activating the greeting animation ability");
    }
}