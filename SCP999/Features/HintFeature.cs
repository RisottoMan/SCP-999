using System.Linq;
using System.Text;
using Exiled.API.Features;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Utilities;
using Scp999.Interfaces;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace Scp999;
public class HintFeature
{
    public static void AddHint(Player player)
    {
        var abilityList = AbilityManager.GetAbilities.OrderBy(r => r.KeyId);
        
        Hint hint = new Hint
        {
            Id = "999",
            AutoText = arg =>
            {
                var cooldown = player.GameObject.GetComponent<CooldownComponent>();
                StringBuilder stringBuilder = new StringBuilder();
                
                stringBuilder.Append("SCP-999 abilities:\n");
                foreach (IAbility ability in abilityList)
                {
                    string color = "yellow";
                    if (!cooldown.IsAbilityAvailable(ability.Name))
                    {
                        color = "red";
                    }
                    
                    stringBuilder.Append($"<color={color}>{ability.Name}  [{ability.KeyCode}]</color>\n");
                }
                
                return stringBuilder.ToString();
            },
            FontSize = 35,
            YCoordinate = 500,
            Alignment = HintAlignment.Right,
            SyncSpeed = HintSyncSpeed.Normal,
        };

        PlayerDisplay playerDisplay = PlayerDisplay.Get(player);
        playerDisplay.AddHint(hint);
    }

    public static void RemoveHint(Player player)
    {
        PlayerDisplay playerDisplay = PlayerDisplay.Get(player);
        playerDisplay.RemoveHint("999");
    }
}