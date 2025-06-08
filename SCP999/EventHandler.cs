using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Warhead;

namespace Scp999;
public class EventHandler
{
    private readonly Plugin _plugin;
    public EventHandler(Plugin plugin)
    {
        _plugin = plugin;

        Exiled.Events.Handlers.Server.RoundStarted += this.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Stopping += this.OnWarheadStop;
        Exiled.Events.Handlers.Scp096.Enraging += this.OnScpEnraging;
        Exiled.Events.Handlers.Scp096.AddingTarget += this.OnAddingTarget;
        Exiled.Events.Handlers.Player.SpawningRagdoll += this.OnSpawningRagdoll;
        Exiled.Events.Handlers.Player.EnteringPocketDimension += this.OnEnteringPocketDimension;
        Exiled.Events.Handlers.Player.SearchingPickup += this.OnSearchingPickup;
        Exiled.Events.Handlers.Player.DroppingItem += this.OnDroppingItem;
        Exiled.Events.Handlers.Player.Hurting += this.OnPlayerHurting;
        Exiled.Events.Handlers.Player.UsingItem += this.OnUsingItem;
        Exiled.Events.Handlers.Player.UsingItem += this.OnUsingItem;
        Exiled.Events.Handlers.Player.InteractingDoor += this.OnInteractingDoor;
        Exiled.Events.Handlers.Player.Dying += this.OnPlayerDying;
        Exiled.Events.Handlers.Player.Left += this.OnPlayerLeft;
        Exiled.Events.Handlers.Player.Verified += this.OnPlayerVerified;
        Exiled.Events.Handlers.Player.ChangingRole += this.OnChangingRole;
        Exiled.Events.Handlers.Player.VoiceChatting += this.OnVoiceChatting;

        //Exiled.Events.Handlers.Player.ChangingSpectatedPlayer += this.ChangingSpectatedPlayer;
    }
    
    ~EventHandler()
    {
        Exiled.Events.Handlers.Server.RoundStarted -= this.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Stopping -= this.OnWarheadStop;
        Exiled.Events.Handlers.Scp096.Enraging -= this.OnScpEnraging;
        Exiled.Events.Handlers.Scp096.AddingTarget -= this.OnAddingTarget;
        Exiled.Events.Handlers.Player.SpawningRagdoll -= this.OnSpawningRagdoll;
        Exiled.Events.Handlers.Player.EnteringPocketDimension -= this.OnEnteringPocketDimension;
        Exiled.Events.Handlers.Player.SearchingPickup -= this.OnSearchingPickup;
        Exiled.Events.Handlers.Player.DroppingItem -= this.OnDroppingItem;
        Exiled.Events.Handlers.Player.Hurting -= this.OnPlayerHurting;
        Exiled.Events.Handlers.Player.UsingItem -= this.OnUsingItem;
        Exiled.Events.Handlers.Player.UsingItem -= this.OnUsingItem;
        Exiled.Events.Handlers.Player.InteractingDoor -= this.OnInteractingDoor;
        Exiled.Events.Handlers.Player.Dying -= this.OnPlayerDying;
        Exiled.Events.Handlers.Player.Left -= this.OnPlayerLeft;
        Exiled.Events.Handlers.Player.Verified -= this.OnPlayerVerified;
        Exiled.Events.Handlers.Player.ChangingRole -= this.OnChangingRole;
        Exiled.Events.Handlers.Player.VoiceChatting -= this.OnVoiceChatting;
        
        //Exiled.Events.Handlers.Player.ChangingSpectatedPlayer -= this.ChangingSpectatedPlayer;
    }
    
    /// <summary>
    /// The logic of choosing SCP-999 if the round is started
    /// </summary>
    private void OnRoundStarted()
    {
        Scp999Role? customRole = CustomRole.Get(typeof(Scp999Role)) as Scp999Role;
        
        foreach (Player player in Player.List)
        {
            // If there is no SCP-999 in the game, then add
            if (customRole!.TrackedPlayers.Count >= customRole.SpawnProperties.Limit)
                return;
            
            // The player already has a role
            if (customRole.Check(player))
                return;

            // The player is an NPC
            if (player.IsNPC && player.Nickname != "SCP-999")
                return;
            
            // Checking the chance to spawn
            if (UnityEngine.Random.Range(0, 100) > customRole.SpawnChance)
                return;
            
            customRole.AddRole(player);
        }
    }
    
    /// <summary>
    /// Allow the use of abilities for SCP-999
    /// </summary>
    private void OnUsingItem(UsingItemEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// The mechanics of opening doors
    /// </summary>
    private void OnInteractingDoor(InteractingDoorEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Allow the attacker to show the hitmarker
    /// </summary>
    private void OnPlayerHurting(HurtingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to pick up items
    /// </summary>
    private void OnSearchingPickup(SearchingPickupEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    /// <summary>
    /// Does not allow SCP-999 to drop items
    /// </summary>
    private void OnDroppingItem(DroppingItemEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Clearing the inventory if the SCP-999 dies
    /// </summary>
    private void OnPlayerDying(DyingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.Player.ClearInventory();
        }
    }
    
    /// <summary>
    /// If the player leaves the game, then delete his instance
    /// </summary>
    private void OnPlayerLeft(LeftEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            CustomRole.Get(typeof(Scp999Role))!.RemoveRole(ev.Player);
        }
    }
    
    /// <summary>
    /// If a player has changed his class, then delete his instance
    /// </summary>
    private void OnChangingRole(ChangingRoleEventArgs ev)
    {
        //Log.Debug("Player change role");
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            //CustomRole.Get(typeof(Scp999Role))!.RemoveRole(ev.Player);
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to turn off the warhead
    /// </summary>
    private void OnWarheadStop(StoppingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to enrage SCP-096
    /// </summary>
    private void OnScpEnraging(EnragingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not add SCP-999 for SCP-096 to targets
    /// </summary>
    private void OnAddingTarget(AddingTargetEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// If the SCP-999 dies, then his original body should not appear
    /// </summary>
    private void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-106 to teleport SCP-999 to a pocket dimension
    /// </summary>
    private void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    /// <summary>
    /// Does not allow SCP-999 to talk in voice chat
    /// </summary>
    private void OnVoiceChatting(VoiceChattingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// If a player has spawned with a new role, then delete his instance
    /// </summary>
    private void OnPlayerVerified(VerifiedEventArgs ev)
    {
        foreach (Player scp999 in Player.List)
        {
            if (CustomRole.Get(typeof(Scp999Role)).Check(scp999))
            {
                InvisibleFeature.MakeInvisibleForPlayer(scp999, ev.Player);
            }
        }
    }

    private void ChangingSpectatedPlayer(ChangingSpectatedPlayerEventArgs ev)
    {
        Log.Error($"{ev.Player.Nickname} {ev.NewTarget} {ev.OldTarget}");
        var scp999Role = CustomRole.Get(typeof(Scp999Role));
        
        if (scp999Role.Check(ev.NewTarget))
        {
            Log.Error($"remove {ev.NewTarget.CustomName}");
            InvisibleFeature.RemoveInvisibleForPlayer(ev.NewTarget, ev.Player);
        }
        
        if (scp999Role.Check(ev.OldTarget))
        {
            Log.Error($"make {ev.NewTarget.CustomName}");
            InvisibleFeature.MakeInvisibleForPlayer(ev.OldTarget, ev.Player);
        }
    }
}