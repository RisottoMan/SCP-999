using System;
using System.IO;
using HarmonyLib;
using Exiled.CustomRoles.API;
using Exiled.API.Features;

namespace Scp999;
public class Plugin : Plugin<Config>
{
    public override string Name => "Scp999";
    public override string Author => "RisottoMan";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredExiledVersion => new(9, 6, 0);
    
    private Harmony _harmony;
    private EventHandler _eventHandler;
    public static Plugin Singleton;
    
    // Configs path
    public string BasePath { get; set; }
    public string SchematicPath { get; set; }
    public string AudioPath { get; set; }

    public override void OnEnabled()
    {
        //CosturaUtility.Initialize();
        
        Singleton = this;
        _eventHandler = new EventHandler();

        // Patch
        _harmony = new Harmony($"risottoman.scp999");
        _harmony.PatchAll();
        
        // Register the custom scp999 role
        Config.Scp999RoleConfig.Register();
        
        // Create config folders
        BasePath = Path.Combine(Paths.IndividualConfigs, this.Name.ToLower());
        SchematicPath = Path.Combine(BasePath, "Schematics");
        AudioPath = Path.Combine(BasePath, "Audio");
        this.CreatePluginDirectory(BasePath);
        this.CreatePluginDirectory(SchematicPath);
        this.CreatePluginDirectory(AudioPath);

        // Register the abilities
        AbilityFeature.RegisterAbilities();
        
        // Event handlers
        Exiled.Events.Handlers.Server.RoundStarted += _eventHandler.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Stopping += _eventHandler.OnWarheadStop;
        Exiled.Events.Handlers.Scp096.Enraging += _eventHandler.OnScpEnraging;
        Exiled.Events.Handlers.Scp096.AddingTarget += _eventHandler.OnAddingTarget;
        Exiled.Events.Handlers.Player.SpawningRagdoll += _eventHandler.OnSpawningRagdoll;
        Exiled.Events.Handlers.Player.EnteringPocketDimension += _eventHandler.OnEnteringPocketDimension;
        Exiled.Events.Handlers.Player.SearchingPickup += _eventHandler.OnSearchingPickup;
        Exiled.Events.Handlers.Player.DroppingItem += _eventHandler.OnDroppingItem;
        Exiled.Events.Handlers.Player.Hurting += _eventHandler.OnPlayerHurting;
        Exiled.Events.Handlers.Player.UsingItem += _eventHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.UsingItem += _eventHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.InteractingDoor += _eventHandler.OnInteractingDoor;
        Exiled.Events.Handlers.Player.Dying += _eventHandler.OnPlayerDying;
        Exiled.Events.Handlers.Player.Left += _eventHandler.OnPlayerLeft;
        Exiled.Events.Handlers.Player.Spawning += _eventHandler.OnPlayerSpawning;
        Exiled.Events.Handlers.Player.ChangingRole += _eventHandler.OnChangingRole;
        Exiled.Events.Handlers.Player.VoiceChatting += _eventHandler.OnVoiceChatting;
        
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Server.RoundStarted -= _eventHandler.OnRoundStarted;
        Exiled.Events.Handlers.Warhead.Stopping -= _eventHandler.OnWarheadStop;
        Exiled.Events.Handlers.Scp096.Enraging -= _eventHandler.OnScpEnraging;
        Exiled.Events.Handlers.Scp096.AddingTarget -= _eventHandler.OnAddingTarget;
        Exiled.Events.Handlers.Player.SpawningRagdoll -= _eventHandler.OnSpawningRagdoll;
        Exiled.Events.Handlers.Player.EnteringPocketDimension -= _eventHandler.OnEnteringPocketDimension;
        Exiled.Events.Handlers.Player.SearchingPickup -= _eventHandler.OnSearchingPickup;
        Exiled.Events.Handlers.Player.DroppingItem -= _eventHandler.OnDroppingItem;
        Exiled.Events.Handlers.Player.Hurting -= _eventHandler.OnPlayerHurting;
        Exiled.Events.Handlers.Player.UsingItem -= _eventHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.UsingItem -= _eventHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.InteractingDoor -= _eventHandler.OnInteractingDoor;
        Exiled.Events.Handlers.Player.Dying -= _eventHandler.OnPlayerDying;
        Exiled.Events.Handlers.Player.Left -= _eventHandler.OnPlayerLeft;
        Exiled.Events.Handlers.Player.Spawning -= _eventHandler.OnPlayerSpawning;
        Exiled.Events.Handlers.Player.ChangingRole -= _eventHandler.OnChangingRole;
        Exiled.Events.Handlers.Player.VoiceChatting -= _eventHandler.OnVoiceChatting;
        
        AbilityFeature.UnregisterAbilities();
        
        Config.Scp999RoleConfig.Unregister();
        _harmony.UnpatchAll();

        _eventHandler = null;
        Singleton = null;
        
        base.OnDisabled();
    }

    private void CreatePluginDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}