using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;
using CustomPlayerEffects;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using MEC;
using YamlDotNet.Serialization;

namespace Scp999;
public class Scp999Role : CustomRole
{
    public override string Name { get; set; } = "SCP-999";
    public override string Description { get; set; } = "The tickle monster";
    
    [Description("The custom tag that you can change. Default: SCP-999")]
    public override string CustomInfo { get; set; } = "SCP-999";
    public override uint Id { get; set; } = 9999;
    public override int MaxHealth { get; set; } = 2000;
    
    [Description("You can choose your own spawn location. You can also increase the number of SCP-999 per round. Default: 1")]
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
    
    [Description("I advise you not to change it. The SCP-999 is neutral. Default: Tutorial")]
    public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
    
    [Description("Broadcast after SCP-999 spawn")]
    public override Exiled.API.Features.Broadcast Broadcast { get; set; } = new()
    {
        Show = true,
        Content = "<color=#ffa500>\ud83d\ude04 You are SCP-999 - The tickle monster! \ud83d\ude04\n" +
                "Heal Humans, dance and calm down SCPs in facility\n" +
               "Use abilities by clicking on the buttons</color>",
        Duration = 15
    };
    
    [Description("Broadcast after SCP-999 spawn")]
    public override string ConsoleMessage { get; set; } = "You are SCP-999 - The tickle monster!\n" +
                                                          "You have a lot of abilities, for example, you can heal players or dance.\n" +
                                                          "Configure your buttons in the settings. Remove the stars.";
    
    /// <summary>
    /// Adding the SCP-999 role to the player
    /// </summary>
    /// <param name="player">The player who should become SCP-999</param>
    public override void AddRole(Player player)
    {
        // Setup of a custom role
        base.AddRole(player);
        player.CustomName = this.CustomInfo;
        player.EnableEffect<Disabled>();
        
        Timing.CallDelayed(0.1f, () =>
        {
            player.EnableEffect<Ghostly>();
        });
        
        // Register PlayerComponent for player
        player.GameObject.AddComponent<PlayerComponent>();
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
        Object.Destroy(player.GameObject.GetComponent<PlayerComponent>());
    }
    
    // These are unnecessary parameters that I don't want to put in the config
    [YamlIgnore]
    public override List<string> Inventory { get; set; }
    
    [YamlIgnore]
    public override List<CustomAbility> CustomAbilities { get; set; }
    
    [YamlIgnore]
    public override Dictionary<AmmoType, ushort> Ammo { get; set; }
    
    [YamlIgnore]
    public override bool KeepPositionOnSpawn { get; set; } = true;
    
    [YamlIgnore]
    public override bool KeepInventoryOnSpawn { get; set; } = true;
    
    [YamlIgnore]
    public override bool RemovalKillsPlayer { get; set; } = true;
    
    [YamlIgnore]
    public override bool KeepRoleOnDeath { get; set; } = false;

    [YamlIgnore] 
    public override float SpawnChance { get; set; } = 0;
    
    [YamlIgnore]
    public override bool IgnoreSpawnSystem { get; set; } = true;
    
    [YamlIgnore]
    public override bool KeepRoleOnChangingRole { get; set; } = false;

    [YamlIgnore]
    public override bool DisplayCustomItemMessages { get; set; }

    [YamlIgnore]
    public override Vector3 Scale { get; set; } = Vector3.one;

    [YamlIgnore]
    public override Vector3? Gravity { get; set; }
    
    [YamlIgnore]
    public override Dictionary<RoleTypeId, float> CustomRoleFFMultiplier { get; set; } = new();

    [YamlIgnore]
    public override string AbilityUsage { get; set; }
}