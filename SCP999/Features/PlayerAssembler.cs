using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using ProjectMER.Features.Objects;
using Scp999.Features.Manager;
using UnityEngine;

namespace Scp999.Features;
public class PlayerAssembler : MonoBehaviour
{
    /// <summary>
    /// Register features for the player
    /// </summary>
    void Awake()
    {
        if (!Player.TryGet(gameObject, out this._player))
            return;
        
        KeybindManager.RegisterKeybindsForPlayer(this._player);                               // Register keybinds
        HintManager.AddHint(this._player);                                                    // Add hint
        AudioManager.AddAudioPlayer(this._player, out this._audioPlayer);                     // Attach a AudioPlayer to the player
        SchematicManager.AddSchematic(this._player, out this._schematicObject);               // Attach a schematic to the player
        SchematicManager.GetAnimatorFromSchematic(this._schematicObject, out this._animator); // Get animator from schematic
        Log.Debug($"[PlayerAssembler] Custom role granted for {this._player.Nickname}");
        
        this._abilityCooldown = AbilityManager.GetAbilities.ToDictionary(a => a.Name, _ => 0f);
        InvokeRepeating(nameof(CheckCooldown), 0f, 1f);                          // Invoke the cooldown cycle
        Log.Debug($"[PlayerAssembler] Invoke the cooldown cycle");
    }

    /// <summary>
    /// Cycle that counts every second of an ability's cooldown
    /// </summary>
    void CheckCooldown()
    {
        foreach (var key in this._abilityCooldown.Keys.ToList())
        {
            if (this._abilityCooldown[key] > 0)
            {
                this._abilityCooldown[key]--;
            }
            else
            {
                this._abilityCooldown[key] = 0;
            }
        }
    }
    
    /// <summary>
    /// Unregister features for the player
    /// </summary>
    void OnDestroy()
    {
        KeybindManager.UnregisterKeybindsForPlayer(this._player);               // Unregister keybinds
        HintManager.RemoveHint(this._player);                                   // Remove hint
        AudioManager.RemoveAudioPlayer(this._audioPlayer);                      // Remove a AudioPlayer
        SchematicManager.RemoveSchematic(this._player, this._schematicObject);  // Remove schematic
        
        Log.Debug($"[PlayerAssembler] Custom role removed for {this._player.Nickname}");
        
        CancelInvoke(nameof(CheckCooldown));                                    // Cancel the cooldown cycle
        Log.Debug($"[PlayerAssembler] Cancel the cooldown cycle");
    }
    
    // Properties
    public Animator GetCurrentAnimator => this._animator;
    public AudioPlayer GetCurrentAudioPlayer => this._audioPlayer;
    public bool IsAbilityAvailable(string ability) => this._abilityCooldown[ability] <= 0;
    public void SetCooldownForAbility(string ability, float time) => this._abilityCooldown[ability] = time;

    // Fields
    private Player _player;
    private SchematicObject _schematicObject;
    private Animator _animator;
    private AudioPlayer _audioPlayer;
    private Dictionary<string, float> _abilityCooldown;
}