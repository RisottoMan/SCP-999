using Exiled.API.Features;
using Scp999.Interfaces;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;
public class FaceAbility : IAbility
{
    public string Name { get; } = "Face";
    public string Description { get; } = "Change the facial animation";
    public int KeyId { get; } = 9995;
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
        
        //ChangeAnimation();
        
        Log.Debug("[HealAbility] Activating the Face ability");
    }
}