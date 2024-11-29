using System;
using PlayerRoles;
using CommandSystem;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Permissions.Extensions;

namespace SCP999;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class SCP999Command : ICommand
{
    public string Command { get; } = "scp999";
    public string[] Aliases { get; } = { "scp999 (me / id / all)" };
    public string Description { get; } = "Make a player (id / all) become SCP-999.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!((CommandSender)sender).CheckPermission(".scp999"))
        {
            response = "You lack permission to do that, required: .scp999";
            return false;
        }

        if (arguments.Count != 1)
        {
            response = "Invalid arguments. Try: scp999 (me / id / all)";
            return false;
        }

        var scp999Role = CustomRole.Get(typeof(Scp999Role));
        if (scp999Role == null)
        {
            response = "<color=red>SCP-999 role not found or not registered.</color>";
            return false;
        }

        switch (arguments.At(0))
        {
            case "*":
            case "all":
                foreach (Player pl in Player.List)
                {
                    if (pl.Role == RoleTypeId.Spectator || pl.Role == RoleTypeId.None)
                        continue;

                    scp999Role.AddRole(pl);
                }
                response = "<color=green>Players spawned as SCP-999.</color>";
                return true;

            case "me":
                if (!((CommandSender)sender).CheckPermission(".scp999me"))
                {
                    response = "<color=red>You lack permission to do that.</color>";
                    return false;
                }

                var player = Player.Get(sender);
                if (player == null)
                {
                    response = "<color=red>Could not find the player associated with the command sender.</color>";
                    return false;
                }

                scp999Role.AddRole(player);
                response = "<color=green>You have been spawned as SCP-999.</color>";
                return true;

            default:
                Player ply = Player.Get(arguments.At(0));
                if (ply == null)
                {
                    response = $"<color=red>Player not found: {arguments.At(0)}</color>";
                    return false;
                }

                if (ply.IsDead)
                {
                    response = "<color=red>SCP999: The player is dead.</color>";
                    return false;
                }

                scp999Role.AddRole(ply);
                response = "<color=green>Player spawned as SCP-999.</color>";
                return true;
        }
    }
}