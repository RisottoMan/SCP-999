using HarmonyLib;
using PluginAPI.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SCP999;

[HarmonyPatch(typeof(Player))]
internal class PlayerList
{
    public static MethodInfo TargetMethod()
    {
        return typeof(Player).GetMethods().First(r => r.Name == "GetPlayers");
    }

    public static void Postfix(ref List<Player> __result)
    {
        if (AudioPlayer.AudioBot == null) return;

        Player dummy = Player.Get(AudioPlayer.AudioBot);
        __result.Remove(dummy);
    }
}