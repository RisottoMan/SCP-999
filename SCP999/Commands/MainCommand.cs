using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace Scp999.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]

public class MainCommand : ParentCommand
{
    public override string Command { get; } = "scp999";
    public override string Description { get; } = "Give for player custom role SCP-999";
    public override string[] Aliases => [];
    
    public MainCommand() => LoadGeneratedCommands();
    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new GiveCommand());
        RegisterCommand(new RemoveCommand());
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!((CommandSender)sender).CheckPermission(".scp999"))
        {
            response = "You do not have permission to use this command!";
            return false;
        }

        response = "Please enter a valid subcommand: give, remove";
        return false;
    }
}