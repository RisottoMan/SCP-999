using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.CustomRoles.API.Features;
using Scp999.Interfaces;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;

public class AnimationAbility : IAbility
{
    public string Name { get; } = "Random Animation";
    public string Description { get; } = "Play a random funny animation";
    public KeyCode KeyBind { get; } = KeyCode.T;
    private IEnumerable<SettingBase> _settings;
    
    public void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += KeybindActivateAbility;

        _settings = new[]
        {
            new KeybindSetting(9994, 
                "[SCP-999] Funny animation", 
                this.KeyBind, 
                hintDescription: "Play a random funny animation. You will not be able to move during the animation.")
        };
        SettingBase.Register(_settings);
    }

    public void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= KeybindActivateAbility;
    }
    
    private static void KeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != 9994 || !keybindSetting.SyncIsPressed)
            return;
        
        if (!Player.TryGet(referenceHub, out Player player))
            return;
        
        if (CustomRole.Get(typeof(Scp999Role))!.Check(player))
            return;

        //PlayAnimation();
        
        Log.Debug("[HealAbility] Activating the random animation ability");
    }
}