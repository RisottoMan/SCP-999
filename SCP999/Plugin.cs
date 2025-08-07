using System;
using Exiled.CustomRoles.API;
using Exiled.API.Features;
using Scp999.Configs;

namespace Scp999;
public class Plugin : Plugin<Config>
{
    public override string Name => "Scp999";
    public override string Author => "RisottoMan";
    public override Version Version => new(1, 4, 0);
    public override Version RequiredExiledVersion => new(9, 6, 0);
    
    public static Plugin Singleton;
    public override void OnEnabled()
    {
        Singleton = this;
        
        // Setup the RoleAPI
        if (!RoleAPI.Startup.SetupAPI(this.Name))
            return;

        // Register the custom role
        Config.Scp999RoleConfig.Register();
        
        new EventHandler(this);
        
        base.OnEnabled();
    }
}