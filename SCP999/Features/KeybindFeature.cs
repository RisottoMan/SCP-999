using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;

namespace Scp999;
public class KeybindFeature
{
    private static IEnumerable<SettingBase> _settings;
    
    public static void RegisterKeybindsForPlayer(Player player)
    {
        if (_settings is null)
        {
            var settings = new List<SettingBase>();
            
            var header = new HeaderSetting(
                name: "Abilities of SCP-999",
                hintDescription: "Abilities of SCP-999",
                paddling: false
            );
            
            settings.Add(header);
            
            foreach (var ability in AbilityManager.GetAbilities.OrderBy(r => r.KeyId))
            {
                var keybindSetting = new KeybindSetting(
                    id: ability.KeyId,
                    label: ability.Name,
                    suggested: ability.KeyCode,
                    hintDescription: ability.Description
                    //header: header
                );

                settings.Add(keybindSetting);
            }

            _settings = settings;
        }
        
        SettingBase.Register(player, _settings);
    }

    public static void UnregisterKeybindsForPlayer(Player player)
    {
        SettingBase.Unregister(player, _settings);
    }
}