using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Scp330;
using Exiled.Events.EventArgs.Warhead;
using Scp999.Features;

namespace Scp999;
public class EventHandler
{
    private readonly Plugin _plugin;
    private Scp999Role _role;
    public EventHandler(Plugin plugin)
    {
        _plugin = plugin;
    
        Exiled.Events.Handlers.Server.RoundStarted += this.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Starting += this.OnWarheadStart;
        Exiled.Events.Handlers.Warhead.Stopping += this.OnWarheadStop;
        Exiled.Events.Handlers.Scp096.AddingTarget += this.OnAddingTarget;
        Exiled.Events.Handlers.Player.SpawningRagdoll += this.OnSpawningRagdoll;
        Exiled.Events.Handlers.Player.EnteringPocketDimension += this.OnEnteringPocketDimension;
        Exiled.Events.Handlers.Player.SearchingPickup += this.OnSearchingPickup;
        Exiled.Events.Handlers.Player.DroppingItem += this.OnDroppingItem;
        Exiled.Events.Handlers.Player.Hurting += this.OnPlayerHurting;
        Exiled.Events.Handlers.Player.UsingItem += this.OnUsingItem;
        Exiled.Events.Handlers.Player.UsingItem += this.OnUsingItem;
        Exiled.Events.Handlers.Player.Dying += this.OnPlayerDying;
        Exiled.Events.Handlers.Scp330.InteractingScp330 += this.OnInteractingScp330;
    }
    
    private void OnRoundStarted()
    {
        _role = CustomRole.Get(typeof(Scp999Role)) as Scp999Role;
        if (_role is null)
        {
            Log.Error("Custom role not found or not registered");
        }
    }

    /// <summary>
    /// Allow the use of abilities for SCP-999
    /// </summary>
    private void OnUsingItem(UsingItemEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    /// <summary>
    /// Block any damage from players
    /// </summary>
    private void OnPlayerHurting(HurtingEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            // Disable damage from players
            if (!_plugin.Config.IsPlayerCanHurt && ev.Attacker is not null)
            {
                ev.IsAllowed = false;
            }

            // Disable damage from car
            if (ev.DamageHandler.Type == DamageType.Crushed && 
                ev.Player.CurrentRoom.Type == RoomType.Surface)
            {
                ev.IsAllowed = false;
            }
        
            // Disable damage from tesla
            if (ev.DamageHandler.Type == DamageType.Tesla)
            {
                ev.IsAllowed = false;
            }
        
            // Increase damage from decontamination
            if (ev.DamageHandler.Type == DamageType.Decontamination)
            {
                ev.Amount = 300;
            }
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to pick up items
    /// </summary>
    private void OnSearchingPickup(SearchingPickupEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    /// <summary>
    /// Does not allow SCP-999 to drop items
    /// </summary>
    private void OnDroppingItem(DroppingItemEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
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
    /// Does not allow SCP-999 to turn on the warhead
    /// </summary>
    private void OnWarheadStart(StartingEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to turn off the warhead
    /// </summary>
    private void OnWarheadStop(StoppingEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not add SCP-999 for SCP-096 to targets
    /// </summary>
    private void OnAddingTarget(AddingTargetEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// If the SCP-999 dies, then his original body should not appear
    /// </summary>
    private void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-106 to teleport SCP-999 to a pocket dimension
    /// </summary>
    private void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    /// <summary>
    /// Does not allow SCP-999 to take candies
    /// </summary>
    private void OnInteractingScp330(InteractingScp330EventArgs ev)
    {
        if (_role != null && _role.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
}