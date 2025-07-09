using PlayerRoles;
using System.Collections.Generic;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using RoleAPI.API;

namespace Scp999.Features;
public class Scp999Role : ExtendedRole
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
    
    public override string ConsoleMessage { get; set; } =
        "You are SCP-999 - The tickle monster!\n" +    
        "You have a lot of abilities, for example, you can heal players or dance.\n" +
        "Configure your buttons in the settings. Remove the stars.";

    public override List<EffectType> EffectList { get; set; } = new()
    {
        EffectType.Disabled,
        EffectType.Slowness,
        EffectType.Ghostly
    };
}