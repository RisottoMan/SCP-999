using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.CustomRoles.API.Features;
using Scp999.Interfaces;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;

public class HealAbility : IAbility
{
    public string Name { get; } = "Heal";
    public string Description { get; } = "Restores health to players within a radius";
    public KeyCode KeyBind { get; } = KeyCode.R;
    
    private IEnumerable<SettingBase> _settings;
    
    public void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += KeybindActivateAbility;

        _settings = new[]
        {
            new KeybindSetting(9993, 
                "[SCP-999] Heal around", 
                this.KeyBind, 
                hintDescription: "Pressing the button activates the restoration of health around you for all players.")
        };
        SettingBase.Register(_settings);
    }

    public void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= KeybindActivateAbility;
    }
    
    private static void KeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != 9993 || !keybindSetting.SyncIsPressed)
            return;
        
        if (!Player.TryGet(referenceHub, out Player player))
            return;
        
        if (CustomRole.Get(typeof(Scp999Role))!.Check(player))
            return;

        foreach (Player ply in Player.List)
        {
            if (player == ply)
                continue;
            
            if (Vector3.Distance(player.Position, ply.Position) < 5f)
            {
                player.Heal(100);
            }
        }
        
        Log.Debug("[HealAbility] Activating the heal ability");
    }
}