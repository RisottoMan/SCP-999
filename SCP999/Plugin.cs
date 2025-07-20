using System;
using System.IO;
using System.Linq;
using HarmonyLib;
using Exiled.CustomRoles.API;
using Exiled.API.Features;
using Scp999.Configs;

namespace Scp999;
public class Plugin : Plugin<Config>
{
    public override string Name => "Scp999";
    public override string Author => "RisottoMan";
    public override Version Version => new(1, 3, 0);
    public override Version RequiredExiledVersion => new(9, 6, 0);
    
    public static Plugin Singleton;
    public override void OnEnabled()
    {
        Singleton = this;
        new EventHandler(this);

        // Checking that the ProjectMER plugin is loaded on the server
        if (!AppDomain.CurrentDomain.GetAssemblies().Any(x => x.FullName.ToLower().Contains("projectmer")))
        {
            Log.Error("ProjectMER is not installed. Schematics can't spawn the SCP-999 game model.");
            return;
        }
        
        // Register the custom scp999 role
        Config.Scp999RoleConfig.Register();
        
        // Setup the RoleAPI
        RoleAPI.Startup.SetupAPI(this.Name);
        
        // Register the abilities
        RoleAPI.API.Managers.AbilityRegistrator.RegisterAbilities();
        RoleAPI.API.Managers.KeybindManager.RegisterKeybinds(
            RoleAPI.API.Managers.AbilityRegistrator.GetAbilities,
            "SCP-999");
        
        base.OnEnabled();
    }
}