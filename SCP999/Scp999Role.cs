using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using UnityEngine;
using System.Collections.Generic;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using MapEditorReborn.API.Features.Objects;
using YamlDotNet.Serialization;

namespace Scp999;
public class Scp999Role : CustomRole
{
    public override string Name { get; set; } = "SCP-999";
    public override string Description { get; set; } = "The tickle monster.";
    public override string CustomInfo { get; set; } = "SCP-999";
    public override uint Id { get; set; } = 999;
    public override int MaxHealth { get; set; } = 2000;
    public override Vector3 Scale { get; set; } = new(.5f, .5f, .5f);
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
    
    [YamlIgnore]
    public override RoleTypeId Role { get; set; } = RoleTypeId.Tutorial;
    private static SchematicObject _schematicObject;
    private static Animator _animator;
    private static AudioPlayer _audioPlayer;
    
    /// <summary>
    /// Adding the SCP-999 role to the player
    /// </summary>
    /// <param name="player">The player who should become SCP-999</param>
    public override void AddRole(Player player)
    {
        if (player.IsNPC || TrackedPlayers.Count >= SpawnProperties.Limit)
            return;
        
        // Setup of a custom role
        base.AddRole(player);
        player.Role.Set(Role, RoleSpawnFlags.None);
        player.Health = MaxHealth;
        player.IsGodModeEnabled = Plugin.Singleton.Config.IsGodModeEnabled;
        
        // Making the player invisible to all players
        InvisibleFeature.MakeInvisibleForPlayer(player);

        // Register keybinds for player
        KeybindFeature.RegisterKeybindsForPlayer(player);
        
        // Attach a schematic to the player
        SchematicFeature.AddSchematic(player, out _schematicObject);
        
        // Attach a AudioPlayer to the player
        AudioFeature.AddAudioPlayer(player, out _audioPlayer);
    }

    /// <summary>
    /// Remove the role from the player
    /// </summary>
    /// <param name="player">A player who should become normal role</param>
    public override void RemoveRole(Player player)
    {
        player.Scale = Vector3.one;
        
        // Remove player invisibility for all players
        InvisibleFeature.RemoveInvisibleForPlayer(player);
        
        // Unregister keybinds for player
        KeybindFeature.UnregisterKeybindsForPlayer(player);
        
        // Remove schematic to the player
        SchematicFeature.RemoveSchematic(player, _schematicObject);
        
        // Remove a AudioPlayer to the player
        AudioFeature.RemoveAudioPlayer(player, _audioPlayer);
    }
}