using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Warhead;
using MEC;
using Random = UnityEngine.Random;

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
        Exiled.Events.Handlers.Player.Dying += this.OnPlayerDying;
        Exiled.Events.Handlers.Player.Left += this.OnPlayerLeft;
        Exiled.Events.Handlers.Player.Verified += this.OnPlayerVerified;
        Exiled.Events.Handlers.Player.ChangingRole += this.OnChangingRole;
        Exiled.Events.Handlers.Player.ChangingSpectatedPlayer += this.OnChangingSpectatedPlayer;
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
        Exiled.Events.Handlers.Player.Dying -= this.OnPlayerDying;
        Exiled.Events.Handlers.Player.Left -= this.OnPlayerLeft;
        Exiled.Events.Handlers.Player.Verified -= this.OnPlayerVerified;
        Exiled.Events.Handlers.Player.ChangingRole -= this.OnChangingRole;
        Exiled.Events.Handlers.Player.ChangingSpectatedPlayer -= this.OnChangingSpectatedPlayer;
    }
    
    /// <summary>
    /// Logic of choosing SCP-999 if the round is started
    /// </summary>
    private void OnRoundStarted()
    {
        Scp999Role? customRole = CustomRole.Get(typeof(Scp999Role)) as Scp999Role;

        // Minimum and maximum number of Players for the chance of SCP-999 appearing
        float min = _plugin.Config.MinimumPlayers;
        float max = _plugin.Config.MaximumPlayers;
        
        // Add SCP-999 if no in the game
        if (customRole!.TrackedPlayers.Count >= customRole.SpawnProperties.Limit)
            return;
        
        for (int i = 0; i < customRole.SpawnProperties.Limit; i++)
        {
            // List of people who could potentially become SCP-999
            var players = Player.List.Where(r => r.IsHuman && !r.IsNPC && !r.HasCustomName).ToList();
            
            // A minimum of players is required
            if (players.Count < min)
                return;
        
            // The formula for the chance of SCP-999 appearing in a round depends on count of players
            float value = Math.Max(min, Math.Min(max, Player.List.Count));
            float chance = (value - min) / (max - min);
            
            // Checking the chance to spawn in current round
            float randomValue = Random.value;

            Log.Debug($"[OnRoundStarted] Spawn chance {randomValue} >= {chance}");
            
            if (randomValue >= chance)
                return;
        
            // Choosing a random player
            Player randomPlayer = players.RandomItem();

            Timing.CallDelayed(0.1f, () =>
            {
                customRole!.AddRole(randomPlayer);
            });
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
    /// Block any damage from players
    /// </summary>
    private void OnPlayerHurting(HurtingEventArgs ev)
    {
        if (!CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
            return;
        
        // Disable damage from players
        if (!_plugin.Config.IsPlayerCanHurt && ev.Attacker is not null)
        {
            ev.IsAllowed = false;
        }

        // Disable car damage
        if (ev.DamageHandler.Type == DamageType.Crushed)
        {
            ev.IsAllowed = false;
        }
            
        // Increase damage from decontamination
        if (ev.DamageHandler.Type == DamageType.Decontamination)
        {
            ev.Amount = 300;
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
    /// If the player leaves the game, remove SCP-999
    /// </summary>
    private void OnPlayerLeft(LeftEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            CustomRole.Get(typeof(Scp999Role))!.RemoveRole(ev.Player);
        }
    }
    
    /// <summary>
    /// This event is triggered if the player dies or the admin changes his role
    /// This is a convenient event to remove a SCP-999
    /// </summary>
    private void OnChangingRole(ChangingRoleEventArgs ev)
    {
        var scp999Role = CustomRole.Get(typeof(Scp999Role));
        
        if (scp999Role!.Check(ev.Player))
        {
            scp999Role!.RemoveRole(ev.Player);
            return;
        }
        
        foreach (Player scp999 in scp999Role!.TrackedPlayers)
        {
            InvisibleFeature.MakeInvisibleForPlayer(scp999, ev.Player);
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
    /// If a player has spawned with a new role, then delete his instance
    /// </summary>
    private void OnPlayerVerified(VerifiedEventArgs ev)
    {
        var scp999Role = CustomRole.Get(typeof(Scp999Role));
        foreach (Player scp999 in scp999Role!.TrackedPlayers)
        {
            InvisibleFeature.MakeInvisibleForPlayer(scp999, ev.Player);
        }
    }
    
    /// <summary>
    /// Spectators should see SCP-999 in the first person, unlike other players
    /// It works with a delay from the server
    /// </summary>
    private void OnChangingSpectatedPlayer(ChangingSpectatedPlayerEventArgs ev)
    {
        var scp999Role = CustomRole.Get(typeof(Scp999Role));
        
        if (scp999Role!.Check(ev.NewTarget))
        {
            InvisibleFeature.RemoveInvisibleForPlayer(ev.NewTarget, ev.Player);
        }
        
        if (scp999Role!.Check(ev.OldTarget))
        {
            InvisibleFeature.MakeInvisibleForPlayer(ev.OldTarget, ev.Player);
        }
    }
}