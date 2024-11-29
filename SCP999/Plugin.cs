using System;
using HarmonyLib;
using Exiled.CustomRoles.API;
using Exiled.API.Features;

namespace SCP999;
public class Plugin : Plugin<Config, Translation>
{
    public override string Name => "SCP999";
    public override string Author => "AleRabo && RisottoMan";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredExiledVersion => new(8, 11, 0);
    
    private Harmony _harmony;
    private EventHandler _eventHandler;
    public static Plugin Singleton;

    public override void OnEnabled()
    {
        Singleton = this;
        _eventHandler = new EventHandler();

        Config.Scp999RoleConfig.Register();

        _harmony = new Harmony($"SCP999 - {DateTime.Now}");
        _harmony.PatchAll();
        
        Exiled.Events.Handlers.Server.RoundStarted += _eventHandler.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Stopping += _eventHandler.OnWarheadStop;
        Exiled.Events.Handlers.Scp096.Enraging += _eventHandler.OnScpEnraging;
        Exiled.Events.Handlers.Scp096.AddingTarget += _eventHandler.OnAddingTarget;
        Exiled.Events.Handlers.Player.SpawningRagdoll += _eventHandler.OnSpawningRagdoll;
        Exiled.Events.Handlers.Player.EnteringPocketDimension += _eventHandler.OnEnteringPocketDimension;
        Exiled.Events.Handlers.Server.WaitingForPlayers += _eventHandler.OnWaitingRound;
        Exiled.Events.Handlers.Player.SearchingPickup += _eventHandler.OnSeachingPickup;
        Exiled.Events.Handlers.Player.DroppingItem += _eventHandler.OnDroppingItem;
        Exiled.Events.Handlers.Player.Hurting += _eventHandler.OnPlayerHurting;
        Exiled.Events.Handlers.Player.UsingItem += _eventHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.UsingItem += _eventHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.InteractingDoor += _eventHandler.OnInteractingDoor;
        Exiled.Events.Handlers.Player.Dying += _eventHandler.OnPlayerDying;
        Exiled.Events.Handlers.Player.Left += _eventHandler.OnPlayerLeft;
        Exiled.Events.Handlers.Player.Spawning += _eventHandler.OnPlayerSpawning;
        Exiled.Events.Handlers.Player.ChangingRole += _eventHandler.OnChangingRole;
        
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Server.RoundStarted -= _eventHandler.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Stopping -= _eventHandler.OnWarheadStop;
        Exiled.Events.Handlers.Scp096.Enraging -= _eventHandler.OnScpEnraging;
        Exiled.Events.Handlers.Scp096.AddingTarget -= _eventHandler.OnAddingTarget;
        Exiled.Events.Handlers.Player.SpawningRagdoll -= _eventHandler.OnSpawningRagdoll;
        Exiled.Events.Handlers.Player.EnteringPocketDimension -= _eventHandler.OnEnteringPocketDimension;
        Exiled.Events.Handlers.Server.WaitingForPlayers -= _eventHandler.OnWaitingRound;
        Exiled.Events.Handlers.Player.SearchingPickup -= _eventHandler.OnSeachingPickup;
        Exiled.Events.Handlers.Player.DroppingItem -= _eventHandler.OnDroppingItem;
        Exiled.Events.Handlers.Player.Hurting -= _eventHandler.OnPlayerHurting;
        Exiled.Events.Handlers.Player.UsingItem -= _eventHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.UsingItem -= _eventHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.InteractingDoor -= _eventHandler.OnInteractingDoor;
        Exiled.Events.Handlers.Player.Dying -= _eventHandler.OnPlayerDying;
        Exiled.Events.Handlers.Player.Left -= _eventHandler.OnPlayerLeft;
        Exiled.Events.Handlers.Player.Spawning -= _eventHandler.OnPlayerSpawning;
        Exiled.Events.Handlers.Player.ChangingRole -= _eventHandler.OnChangingRole;
        
        Config.Scp999RoleConfig.Unregister();
        _harmony.UnpatchAll();

        _eventHandler = null;
        Singleton = null;
        
        base.OnDisabled();
    }
}