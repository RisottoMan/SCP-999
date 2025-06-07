using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;

namespace Scp999.Commands;
internal class GiveCommand : ICommand
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
        
        var scp999Role = CustomRole.Get(typeof(Scp999Role));
        if (scp999Role == null)
        {
            response = "Custom role SCP-999 role not found or not registered";
            return false;
        }
        
        scp999Role.AddRole(player);
        response = $"<color=green>Custom role SCP-999 granted for {player.Nickname}</color>";
        return true;
    }
}