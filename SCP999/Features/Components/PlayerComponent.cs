using Exiled.API.Features;
using MapEditorReborn.API.Features.Objects;
using UnityEngine;

namespace Scp999;
public class PlayerComponent : MonoBehaviour
{
    public void Register()
    {
        if (!Player.TryGet(gameObject, out this._player))
            return;
        
        // Making the player invisible to all players
        InvisibleFeature.MakeInvisibleForPlayer(this._player);
        
        // Attach a AudioPlayer to the player
        AudioFeature.AddAudioPlayer(this._player, out this._audioPlayer);
        
        // Attach a schematic to the player
        SchematicFeature.AddSchematic(this._player, out this._schematicObject);
        
        if (this._schematicObject is not null)
        {
            // Getting an animator from an existing schematic
            SchematicFeature.GetAnimatorFromSchematic(this._schematicObject, out this._animator);
        
            // Register keybinds for player
            KeybindFeature.RegisterKeybindsForPlayer(this._player);
        }
    }

    public void Unregister()
    {
        // Unregister keybinds for player
        KeybindFeature.UnregisterKeybindsForPlayer(this._player);

        // Remove a AudioPlayer to the player
        AudioFeature.RemoveAudioPlayer(this._audioPlayer);
        
        if (this._schematicObject is not null)
        {
            // Remove schematic to the player
            SchematicFeature.RemoveSchematic(this._player, this._schematicObject);
            
            // Remove player invisibility for all players
            InvisibleFeature.RemoveInvisibleForPlayer(this._player);
        }
        
        Destroy(this);
    }

    public SchematicObject GetCurrentSchematic => this._schematicObject;
    public Animator GetCurrentAnimator => this._animator;
    public AudioPlayer GetCurrentAudioPlayer => this._audioPlayer;

    private Player _player;
    private SchematicObject _schematicObject;
    private Animator _animator;
    private AudioPlayer _audioPlayer;
}