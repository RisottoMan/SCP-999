﻿using System.ComponentModel;
using Exiled.API.Interfaces;
using Scp999.Features;

namespace Scp999.Configs;
public class Config : IConfig
{
    [Description("Whether or not is the plugin enabled?")]
    public bool IsEnabled { get; set; } = true;

    [Description("Whether or not is the plugin is in debug mode?")]
    public bool Debug { get; set; } = false;

    [Description("Can players harm SCP-999? Default: false")]
    public bool IsPlayerCanHurt { get; set; } = false;
    
    [Description("Maximum range of SCP-999 abilities")]
    public float MaxDistance { get; set; } = 10;

    [Description("Configs for the SCP-999 role players turn into")]
    public Scp999Role Scp999RoleConfig { get; set; } = new();
}