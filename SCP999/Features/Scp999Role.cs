using PlayerRoles;
using System.Collections.Generic;
using Exiled.API.Features.Spawn;
using Exiled.API.Enums;
using RoleAPI.API;
using RoleAPI.API.Configs;
using Scp999.Features.Abilities;
using UnityEngine;

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

    public override string CustomDeathText { get; set; } = "There is no text here, as it is a safe class.";

    public override SpawnConfig SpawnConfig { get; set; } = new()
    {
        MinPlayers = 7,
        SpawnChance = 50
    };

    public override SchematicConfig SchematicConfig { get; set; } = new()
    {
        SchematicName = "SCP999",
        Offset = new Vector3(0f, -0.75f, 0f),
    };

    public override TextToyConfig TextToyConfig { get; set; } = new()
    {
        Text = "<color=orange>SCP-999</color>",
        Offset = new Vector3(0, 1, 0),
        Rotation = new Vector3(0, 180, 0),
        Scale = new Vector3(0.2f, 0.2f, 0.2f),
    };

    public override HintConfig HintConfig { get; set; } = new()
    {
        Text = "<align=right><size=50><color=#ffa500>\ud83d\ude06 <b>SCP-999</b></color></size>\n" + 
               "Abilities:\n" + 
               "<color=%color%>Yippee {0}</color>\n" + 
               "<color=%color%>Hello {1}</color>\n" + 
               "<color=%color%>Heal {2}</color>\n" + 
               "<color=%color%>Dance {3}</color>\n" + 
               "\n<size=18>if you cant use abilities\nremove \u2b50 in settings</size></align>",
        AvailableAbilityColor = "#ffa500",
        UnavailableAbilityColor = "#966100"
    };

    public override AudioConfig AudioConfig { get; set; } = new()
    {
        Name = "scp999",
        Volume = 100,
        IsSpatial = true,
        MinDistance = 5f,
        MaxDistance = 15f
    };

    public override AbilityConfig AbilityConfig { get; set; } = new()
    {
        AbilityTypes = 
        [
            typeof(YippeeAbility),
            typeof(HelloAbility),
            typeof(HealAbility),
            typeof(AnimationAbility)
        ]
    };

    public override List<EffectConfig> Effects { get; set; } =
    [
        new EffectConfig()
        {
            EffectType = EffectType.Disabled,
        },

        new EffectConfig()
        {
            EffectType = EffectType.Slowness,
            Intensity = 25
        }
    ];
    
    public override bool IsPlayerInvisible { get; set; } = true;
    public override bool IsShowPlayerNickname { get; set; } = true;
}