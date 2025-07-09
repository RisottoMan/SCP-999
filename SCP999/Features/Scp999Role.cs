using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using MEC;
using Scp999.Features.Controller;
using UnityEngine;

namespace Scp999.Features;
public class Scp999Role : CustomRole
{
    public override string Name { get; set; } = "SCP-999";
    public override string Description { get; set; } = "The tickle monster";
    public override string CustomInfo { get; set; } = "SCP-999";
    public override uint Id { get; set; } = 9999;
    public override int MaxHealth { get; set; } = 2000;
    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        DynamicSpawnPoints = new List<DynamicSpawnPoint>()
        {
            new() { Chance = 25, Location = SpawnLocationType.Inside330Chamber },
            new() { Chance = 25, Location = SpawnLocationType.Inside914 },
            new() { Chance = 25, Location = SpawnLocationType.InsideGr18 },
            new() { Chance = 25, Location = SpawnLocationType.InsideLczWc }
        }
    };
    
    public override bool KeepPositionOnSpawn { get; set; } = true;
    public override bool KeepInventoryOnSpawn { get; set; } = false;
    public override bool RemovalKillsPlayer { get; set; } = true;
    public override bool KeepRoleOnDeath { get; set; } = false;
    public override bool IgnoreSpawnSystem { get; set; } = true;
    public override bool KeepRoleOnChangingRole { get; set; } = false;
    public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
    
    public override Exiled.API.Features.Broadcast Broadcast { get; set; } = new()
    {
        Show = true,
        Content = 
            "<color=#ffa500>\ud83d\ude04 You are SCP-999 - The tickle monster! \ud83d\ude04\n" +
            "Heal Humans, dance and calm down SCPs in facility\n" +
            "Use abilities by clicking on the buttons</color>",
        Duration = 15
    };

    protected override void ShowMessage(Player player) {}
    
    public override string ConsoleMessage { get; set; } =
        "You are SCP-999 - The tickle monster!\n" +    
        "You have a lot of abilities, for example, you can heal players or dance.\n" +
        "Configure your buttons in the settings. Remove the stars.";
    
    /// <summary>
    /// Adding the SCP-999 role to the player
    /// </summary>
    /// <param name="player">The player who should become SCP-999</param>
    public override void AddRole(Player player)
    {
        player.Role.Set(this.Role, SpawnReason.ForceClass, RoleSpawnFlags.None);
        player.Position = SpawnProperties.DynamicSpawnPoints.RandomItem().Position;
        
        player.ClearItems();
        player.ClearAmmo();
        player.UniqueRole = this.Name;
        this.TrackedPlayers.Add(player);
        player.Health = this.MaxHealth;
        player.MaxHealth = this.MaxHealth;
        player.Scale = this.Scale;
        player.CustomName = this.Name;
        player.CustomInfo = player.CustomName + "\n" + this.CustomInfo;
      
        this.ShowMessage(player);
        this.ShowBroadcast(player);
        this.RoleAdded(player);
        player.SendConsoleMessage(this.ConsoleMessage, "green");
      
        player.EnableEffect<Disabled>();
        player.EnableEffect<Slowness>(intensity: 25);
      
        Timing.CallDelayed(0.1f, () =>
        {
            player.EnableEffect<Ghostly>();
        });
        
        // Register PlayerComponent for player
        player.GameObject.AddComponent<PlayerController>();
    }

    /// <summary>
    /// Remove the role from the player
    /// </summary>
    /// <param name="player">A player who should become normal role</param>
    public override void RemoveRole(Player player)
    {
        // Remove a custom role
        base.RemoveRole(player);
        player.CustomName = null;
        
        // Unregister PlayerComponent for player
        Object.Destroy(player.GameObject.GetComponent<PlayerController>());
    }
}