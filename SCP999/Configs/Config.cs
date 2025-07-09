using System.ComponentModel;
using Exiled.API.Interfaces;
using Scp999.Features;
using UnityEngine;

namespace Scp999;
public class Config : IConfig
{
    [Description("Whether or not is the plugin enabled?")]
    public bool IsEnabled { get; set; } = true;

    [Description("Whether or not is the plugin is in debug mode?")]
    public bool Debug { get; set; } = false;

    [Description("Can players harm SCP-999? Default: false")]
    public bool IsPlayerCanHurt { get; set; } = false;
    
    [Description("The volume of all the audio files.")]
    public byte Volume { get; set; } = 100;
    
    [Description("Maximum range of SCP-999 abilities")]
    public float MaxDistance { get; set; } = 10;
    
    [Description("The minimum players required to spawn SCP-999")]
    public int MinimumPlayers { get; set; } = 5;
    
    [Description("The maximum players required to spawn SCP-999")]
    public int MaximumPlayers { get; set; } = 15;
    
    [Description("The name of the schematic that will be used as a game model")]
    public string SchematicName { get; set; } = "SCP999";
    
    [Description("Offset the position of the schematic relative to the player")]
    public Vector3 SchematicOffset { get; set; } = new Vector3(0f, -0.75f, 0f);

    [Description("Configs for the SCP-999 role players turn into")]
    public Scp999Role Scp999RoleConfig { get; set; } = new();
}