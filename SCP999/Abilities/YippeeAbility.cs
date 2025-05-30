using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.CustomRoles.API.Features;
using Scp999.Interfaces;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;

public class YippeeAbility : IAbility
{
    public string Name { get; } = "Yippee";
    public string Description { get; } = "Play the Yippee sound";
    public KeyCode KeyBind { get; } = KeyCode.Q;
    
    private IEnumerable<SettingBase> _settings;
    
    public void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += KeybindActivateAbility;

        _settings = new[]
        {
            new KeybindSetting(9990, 
                "[SCP-999] Yippee sound", 
                this.KeyBind, 
                hintDescription: "When you click on this button, you will play the <i>Yippee</i> sound that all players will hear.")
        };
        SettingBase.Register(_settings);
    }

    public void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= KeybindActivateAbility;
    }
    
    private static void KeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != 9990 || !keybindSetting.SyncIsPressed)
            return;
        
        if (!Player.TryGet(referenceHub, out Player player))
            return;
        
        if (CustomRole.Get(typeof(Scp999Role))!.Check(player))
            return;
        
        //PlaySound();
        
        Log.Debug("[HealAbility] Activating the Yippee ability");
    }
}