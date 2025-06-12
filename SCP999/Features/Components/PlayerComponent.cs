using Exiled.API.Features;
using MapEditorReborn.API.Features.Objects;
using UnityEngine;

namespace Scp999;
public class PlayerComponent : MonoBehaviour
{
    /// <summary>
    /// Register features for the player
    /// </summary>
    void Awake()
    {
        if (!Player.TryGet(gameObject, out this._player))
            return;
        
        // Add cooldown for abilities
        this._player.GameObject.AddComponent<CooldownComponent>();
        
        // Register keybinds for player
        KeybindFeature.RegisterKeybindsForPlayer(this._player);
        
        // Add hint for player
        HintFeature.AddHint(this._player);
        
        // Attach a AudioPlayer to the player
        AudioFeature.AddAudioPlayer(this._player, out this._audioPlayer);
        
        // Attach a schematic to the player
        SchematicFeature.AddSchematic(this._player, out this._schematicObject);
        
        if (this._schematicObject is not null)
        {
            // Making the player invisible to all players
            InvisibleFeature.MakeInvisible(this._player);
            
            // Getting an animator from an existing schematic
            SchematicFeature.GetAnimatorFromSchematic(this._schematicObject, out this._animator);
        }
        
        Log.Debug($"[PlayerComponent] Custom role granted for {this._player.Nickname}");
    }

    /// <summary>
    /// Unregister features for the player
    /// </summary>
    void OnDestroy()
    {
        // Destroy cooldown for abilities
        Destroy(this._player.GameObject.GetComponent<CooldownComponent>());
        
        // Unregister keybinds for player
        KeybindFeature.UnregisterKeybindsForPlayer(this._player);

        // Add hint for player
        HintFeature.RemoveHint(this._player);
        
        // Remove a AudioPlayer to the player
        AudioFeature.RemoveAudioPlayer(this._audioPlayer);
        
        if (this._schematicObject is not null)
        {
            // Remove player invisibility for all players
            InvisibleFeature.RemoveInvisible(this._player);
            
            // Remove schematic to the player
            SchematicFeature.RemoveSchematic(this._player, this._schematicObject);
        }
        
        Log.Debug($"[PlayerComponent] Custom role removed for {this._player.Nickname}");
    }

    public SchematicObject GetCurrentSchematic => this._schematicObject;
    public Animator GetCurrentAnimator => this._animator;
    public AudioPlayer GetCurrentAudioPlayer => this._audioPlayer;

    private Player _player;
    private SchematicObject _schematicObject;
    private Animator _animator;
    private AudioPlayer _audioPlayer;
}