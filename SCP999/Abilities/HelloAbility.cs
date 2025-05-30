using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.CustomRoles.API.Features;
using Scp999.Interfaces;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;

public class HelloAbility : IAbility
{
    public string Name { get; } = "Hello";
    public string Description { get; } = "Greeting players or attracting attention";
    public KeyCode KeyBind { get; } = KeyCode.E;
    private IEnumerable<SettingBase> _settings;
    
    public void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += KeybindActivateAbility;

        _settings = new[]
        {
            new KeybindSetting(9992, 
                "[SCP-999] Greeting animation", 
                this.KeyBind, 
                hintDescription: "When you click on this button, you will greet the players or ask for attention.")
        };
        SettingBase.Register(_settings);
    }

    public void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= KeybindActivateAbility;
    }
    
    private static void KeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != 9992 || !keybindSetting.SyncIsPressed)
            return;
        
        if (!Player.TryGet(referenceHub, out Player player))
            return;
        
        if (CustomRole.Get(typeof(Scp999Role))!.Check(player))
            return;

        //PlayAnimation();
        
        Log.Debug("[HealAbility] Activating the greeting animation ability");
    }
}