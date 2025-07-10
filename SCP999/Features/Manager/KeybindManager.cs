using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features.Core.UserSettings;

namespace Scp999.Features.Manager;
public static class KeybindManager
{
    private static IEnumerable<SettingBase> _settings;
    
    public static void RegisterKeybinds()
    {
        var settings = new List<SettingBase>();
            
        var header = new HeaderSetting(
            name: "Abilities of SCP-999",
            hintDescription: "Abilities of SCP-999",
            paddling: true
        );
            
        settings.Add(header);
            
        foreach (var ability in AbilityManager.GetAbilities.OrderBy(r => r.KeyId))
        {
            var keybindSetting = new KeybindSetting(
                id: ability.KeyId,
                label: ability.Name,
                suggested: ability.KeyCode,
                hintDescription: ability.Description,
                preventInteractionOnGUI: true
                //header: header
            );

            settings.Add(keybindSetting);
        }

        _settings = settings;
        
        SettingBase.Register(_settings);
        SettingBase.SendToAll();
    }

    public static void UnregisterKeybinds()
    {
        SettingBase.Unregister(settings: _settings);
    }
}