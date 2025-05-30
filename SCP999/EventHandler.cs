using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Warhead;

namespace Scp999;
public class EventHandler
{
    /// <summary>
    /// The logic of choosing SCP-999 if the round is started
    /// </summary>
    public void OnRoundStarted()
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
    public void OnUsingItem(UsingItemEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// The mechanics of opening doors - todo
    /// </summary>
    public void OnInteractingDoor(InteractingDoorEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Allow the attacker to show the hitmarker
    /// </summary>
    public void OnPlayerHurting(HurtingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to pick up items
    /// </summary>
    public void OnSearchingPickup(SearchingPickupEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }

    /// <summary>
    /// Does not allow SCP-999 to drop items
    /// </summary>
    public void OnDroppingItem(DroppingItemEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Clearing the inventory if the SCP-999 dies
    /// </summary>
    public void OnPlayerDying(DyingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.Player.ClearInventory();
        }
    }
    
    /// <summary>
    /// If the player leaves the game, then delete his instance
    /// </summary>
    public void OnPlayerLeft(LeftEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            CustomRole.Get(typeof(Scp999Role))!.RemoveRole(ev.Player);
        }
    }
    
    /// <summary>
    /// If a player has spawned with a new role, then delete his instance
    /// </summary>
    public void OnPlayerSpawning(SpawningEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            CustomRole.Get(typeof(Scp999Role))!.RemoveRole(ev.Player);
        }
    }
    
    /// <summary>
    /// If a player has changed his class, then delete his instance
    /// </summary>
    public void OnChangingRole(ChangingRoleEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            CustomRole.Get(typeof(Scp999Role))!.RemoveRole(ev.Player);
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to turn off the warhead
    /// </summary>
    public void OnWarheadStop(StoppingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-999 to enrage SCP-096
    /// </summary>
    public void OnScpEnraging(EnragingEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not add SCP-999 for SCP-096 to targets
    /// </summary>
    public void OnAddingTarget(AddingTargetEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// If the SCP-999 dies, then his original body should not appear
    /// </summary>
    public void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
    
    /// <summary>
    /// Does not allow SCP-106 to teleport SCP-999 to a pocket dimension
    /// </summary>
    public void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
    {
        if (CustomRole.Get(typeof(Scp999Role))!.Check(ev.Player))
        {
            ev.IsAllowed = false;
        }
    }
}