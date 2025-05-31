using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using UnityEngine;

namespace Scp999;
public class KeybindFeature
{
    private static IEnumerable<SettingBase> _settings = new SettingBase[]
    {
        new HeaderSetting("SCP999"),
        new KeybindSetting(9990,
            "Yippee sound",
            KeyCode.Q,
            hintDescription: "When you click on this button, you will play the <i>Yippee</i> sound that all players will hear."),
        new KeybindSetting(9991,
            "Change player size",
            KeyCode.F,
            hintDescription: "When you click on this button, you can change the size of the player you are looking at."),
        new KeybindSetting(9992,
            "Greeting animation",
            KeyCode.E,
            hintDescription: "When you click on this button, you will greet the players or ask for attention."),
        new KeybindSetting(9993,
            "Heal around",
            KeyCode.R,
            hintDescription: "Pressing the button activates the restoration of health around you for all players."),
        new KeybindSetting(9994, 
            "Funny animation", 
            KeyCode.T, 
            hintDescription: "Play a random funny animation. You will not be able to move during the animation."),
        new KeybindSetting(9995, 
            "Change face", 
            KeyCode.Y, 
            hintDescription: "Change the facial animation. You can become evil or kind."),
    };
    
    public static void RegisterKeybindsForPlayer(Player player)
    {
        SettingBase.Register(player, _settings);
    }

    public static void UnregisterKeybindsForPlayer(Player player)
    {
        SettingBase.Unregister(player, _settings);
    }
}