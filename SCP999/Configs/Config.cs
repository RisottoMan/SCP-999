using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCP999;
public class Config : IConfig
{
    [Description("Whether or not is the plugin enabled?")]
    public bool IsEnabled { get; set; } = true;

    [Description("Whether or not is the plugin is in debug mode?")]
    public bool Debug { get; set; } = false;

    [Description("Is SCP-999 immortal")]
    public bool IsGodModeEnabled { get; set; } = false;

    [Description("Configs for the SCP-999 role players turn into.")]
    public Scp999Role Scp999RoleConfig { get; set; } = new();

    [Description("The path where you store all the audio files.")]
    public string AudioPath { get; set; } = "";

    [Description("The volume of all the audio files.")]
    public byte Volume { get; set; } = 100;

    [Description("SCP-999 AOE abiliities range.")]
    public Dictionary<string, AbilityConfig> Abilities { get; set; } = new()
    {
        ["Invigorate"] = new AbilityConfig(10f, 5f, ItemType.SCP207),
        ["Heal"] = new AbilityConfig(20f, 50f, ItemType.Adrenaline),
        ["SpeedBoost"] = new AbilityConfig(15f, 4f, ItemType.Medkit)
    };
    
    [Description("Does the SpeedBoost ability slow SCPs?")]
    public bool SpeedSlowsSCPs { get; set; } = true;
    
    [Description("Maximum range of SCP-999 abilities")]
    public float MaxDistance { get; set; } = 10;
}