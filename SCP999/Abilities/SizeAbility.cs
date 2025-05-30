using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.CustomRoles.API.Features;
using Scp999.Interfaces;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;

public class SizeAbility : IAbility
{
    public string Name { get; } = "Size";
    public string Description { get; } = "Change the size of a specific player";
    public KeyCode KeyBind { get; } = KeyCode.F;
    private IEnumerable<SettingBase> _settings;
    
    public void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += KeybindActivateAbility;

        _settings = new[]
        {
            new KeybindSetting(9991, 
                "[SCP-999] Change player size", 
                this.KeyBind, 
                hintDescription: "When you click on this button, you can change the size of the player you are looking at.")
        };
        SettingBase.Register(_settings);
    }

    public void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= KeybindActivateAbility;
    }
    
    private static void KeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != 9991 || !keybindSetting.SyncIsPressed)
            return;
        
        if (!Player.TryGet(referenceHub, out Player player))
            return;
        
        if (CustomRole.Get(typeof(Scp999Role))!.Check(player))
            return;

        //Raycast
        //ChangeSize
        
        Log.Debug("[HealAbility] Activating the size ability");
    }
}