using Exiled.API.Features;
using ProjectMER.Features.Objects;
using Scp999.Features.Manager;
using UnityEngine;

namespace Scp999.Features.Controller;
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Register features for the player
    /// </summary>
    void Awake()
    {
        _player = Player.Get(gameObject);
        Config config = Plugin.Singleton.Config;
        
        KeybindManager.RegisterKeybindsForPlayer(this._player); // Register keybinds to player
        HintManager.AddHint(this._player);                      // Attach hintservice to player
        InvisibleManager.MakeInvisible(this._player);           // Make player invisible for other players
        
        _schematicObject = SchematicManager.AddSchematicByName(config.SchematicName); // Create schematic
        _animator = SchematicManager.GetAnimatorFromSchematic(this._schematicObject); // Get animator from schematic
        _audioPlayer = AudioManager.AddAudioPlayer(this._player, config.Volume);      // Create audioPlayer
        _audioPlayer.TryGetSpeaker("scp999-speaker", out Speaker speaker);       // Get speaker

        _movementController = gameObject.AddComponent<MovementController>();
        _movementController.Init(_schematicObject, speaker, config.SchematicOffset);
        _cooldownController = gameObject.AddComponent<CooldownController>();
        
        Log.Debug($"[PlayerController] Custom role granted for {this._player.Nickname}");
    }
    
    /// <summary>
    /// Unregister features for the player
    /// </summary>
    void OnDestroy()
    {
        Destroy(_movementController); // Destroy movement controller for schematic and audio
        Destroy(_cooldownController); // Destroy cooldown for abilities

        InvisibleManager.RemoveInvisible(this._player);           // Remove invisible
        KeybindManager.UnregisterKeybindsForPlayer(this._player); // Unregister keybinds
        HintManager.RemoveHint(this._player);                     // Remove hint
        _audioPlayer.RemoveAllClips();                            // Remove all audio clips
        _audioPlayer.Destroy();                                   // Remove a AudioPlayer
        this._schematicObject.Destroy();                          // Remove schematic
        
        Log.Debug($"[PlayerController] Custom role removed for {this._player.Nickname}");
    }
    
    // Properties
    public Animator GetCurrentAnimator => this._animator;
    public AudioPlayer GetCurrentAudioPlayer => this._audioPlayer;

    // Fields
    private Player _player;
    private SchematicObject _schematicObject;
    private Animator _animator;
    private AudioPlayer _audioPlayer;
    private MovementController _movementController;
    private CooldownController _cooldownController;
}