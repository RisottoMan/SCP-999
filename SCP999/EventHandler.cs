using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Warhead;
using MEC;
using Scp999.Features;
using Random = UnityEngine.Random;

namespace Scp999;
public class EventHandler
{
    private readonly Plugin _plugin;
    public EventHandler(Plugin plugin)
    {
        _plugin = plugin;
    
        Exiled.Events.Handlers.Server.RoundStarted += this.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Starting += this.OnWarheadStart;
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
    }
    
    ~EventHandler()
    {
        Exiled.Events.Handlers.Server.RoundStarted -= this.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Starting -= this.OnWarheadStart;
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
    }
    
    /// <summary>
    /// After starting the round, select SCP-999
    /// </summary>
    private void OnRoundStarted()
    {
        Timing.CallDelayed(0.1f, this.SpawnRandomPlayer);
    }

    /// <summary>
    /// Logic of choosing SCP-999 if the round is started
    /// </summary>
    private void SpawnRandomPlayer()
    {
        Scp999Role customRole = CustomRole.Get(9999) as Scp999Role;

        // Minimum and maximum number of Players for the chance of SCP-999 appearing
        float min = _plugin.Config.MinimumPlayers - 1;
        float max = _plugin.Config.MaximumPlayers;

        if (min < 0 || max < 0)
        {
            Log.Error("Set the number of players to normal values in config");
            return;
        }
        
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
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    /// <summary>
    /// Block any damage from players
    /// </summary>
    private void OnPlayerHurting(HurtingEventArgs ev)
    {
        if (!CustomRole.Get(9999)!.Check(ev.Player))
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
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    /// <summary>
    /// Does not allow SCP-999 to drop items
    /// </summary>
    private void OnDroppingItem(DroppingItemEventArgs ev)
    {
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Clearing the inventory if the SCP-999 dies
    /// </summary>
    private void OnPlayerDying(DyingEventArgs ev)
    {
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.Player.ClearInventory();
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to turn on the warhead
    /// </summary>
    private void OnWarheadStart(StartingEventArgs ev)
    {
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to turn off the warhead
    /// </summary>
    private void OnWarheadStop(StoppingEventArgs ev)
    {
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to enrage SCP-096
    /// </summary>
    private void OnScpEnraging(EnragingEventArgs ev)
    {
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not add SCP-999 for SCP-096 to targets
    /// </summary>
    private void OnAddingTarget(AddingTargetEventArgs ev)
    {
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// If the SCP-999 dies, then his original body should not appear
    /// </summary>
    private void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
    {
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-106 to teleport SCP-999 to a pocket dimension
    /// </summary>
    private void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
    {
        if (CustomRole.Get(9999)!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
}