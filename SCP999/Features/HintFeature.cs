using System.Collections.Generic;
using System.Text;
using Exiled.API.Enums;
using Exiled.API.Features;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Utilities;
using Scp999.Interfaces;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace Scp999;
public class HintFeature
{
    public static void AddHint(Player player)
    {
        List<IAbility> abilityList = AbilityFeature.GetAvailableAbilities;
        StringBuilder stringBuilder = new StringBuilder();
        
        foreach (IAbility ability in abilityList)
        {
            stringBuilder.Append(ability.Name + "\n");
        }
        
        Hint hint = new Hint
        {
            Text = stringBuilder.ToString(),
            FontSize = 40,
            YCoordinate = 700,
            Alignment = HintAlignment.Left
        };

        PlayerDisplay playerDisplay = PlayerDisplay.Get(player);Room.Get(RoomType.Surface)
        playerDisplay.AddHint(hint);
    }

    public static void RemoveHint(Player player)
    {
        //player.RemoveHint("");
    }
}