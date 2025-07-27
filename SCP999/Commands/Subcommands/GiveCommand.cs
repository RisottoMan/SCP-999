using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Scp999.Features;

namespace Scp999.Commands;
public class GiveCommand : ICommand
{
    public string Command => "give";
    public string Description => "Give a custom role SCP-999 for player";
    public string[] Aliases => [];
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count != 1)
        {
            response = $"Specify the player id to the command: scp999 give [id]";
            return false;
        }
        
        Player player = Player.Get(arguments.At(0));
        if (player == null)
        {
            response = $"Player not found: {arguments.At(0)}";
            return false;
        }
        
        if (player.CustomInfo is not null)
        {
            response = "The player already have the custom role";
            return false;
        }
        
        var scp999Role = CustomRole.Get(typeof(Scp999Role));
        if (scp999Role == null)
        {
            response = "Custom role SCP-999 role not found or not registered";
            return false;
        }

        if (scp999Role.Check(player))
        {
            response = "The player already have the custom role SCP-999";
            return false;
        }
        
        scp999Role.AddRole(player);
        response = $"<color=green>Custom role SCP-999 granted for {player.Nickname}</color>";
        return true;
    }
}