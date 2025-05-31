using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Scp999;
public class Config : IConfig
{
    [Description("Whether or not is the plugin enabled?")]
    public bool IsEnabled { get; set; } = true;

    [Description("Whether or not is the plugin is in debug mode?")]
    public bool Debug { get; set; } = false;

    [Description("Is SCP-999 immortal")]
    public bool IsGodModeEnabled { get; set; } = true;
    
    [Description("The volume of all the audio files.")]
    public byte Volume { get; set; } = 100;
    
    [Description("Maximum range of SCP-999 abilities")]
    public float MaxDistance { get; set; } = 10;

    [Description("Configs for the SCP-999 role players turn into.")]
    public Scp999Role Scp999RoleConfig { get; set; } = new();
}