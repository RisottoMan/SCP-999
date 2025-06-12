using Exiled.API.Features;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999.Interfaces;
public abstract class Ability : IAbility
{
    public virtual string Name { get; }
    public virtual string Description { get; }
    public virtual int KeyId { get; }
    public virtual KeyCode KeyCode { get; }
    public virtual float Cooldown { get; }
    public virtual void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += OnKeybindActivateAbility;
    }

    public virtual void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= OnKeybindActivateAbility;
    }

    private void OnKeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        // Check keybind settings
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != this.KeyId || !keybindSetting.SyncIsPressed)
            return;
        
        // Check player
        if (!Player.TryGet(referenceHub, out Player player))
            return;
        
        // Check current animation
        Animator animator = player.GameObject.GetComponent<PlayerComponent>().GetCurrentAnimator;
        if (animator is not null)
        {
            // If the current animation is not idle, then in progress
            // I would like the animation of the ability to stop, as otherwise it will be possible to play multiple animations at a time
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("IdleAnimation"))
                return;
        }
        
        // Check current audio
        AudioPlayer audioPlayer = player.GameObject.GetComponent<PlayerComponent>().GetCurrentAudioPlayer;
        /*
        if (audioPlayer is not null)
        {
            if (audioPlayer.)
                return;
        }*/
        
        // Check cooldown for the ability
        CooldownComponent cooldown = player.GameObject.GetComponent<CooldownComponent>();
        if (!cooldown.IsAbilityAvailable(this.Name))
            return;
        
        // Set a cooldown for the ability
        cooldown.SetCooldownForAbility(this.Name, this.Cooldown);
        
        // Activate the ability
        this.ActivateAbility(player, animator, audioPlayer);
        Log.Debug($"[Ability] Activating the {this.Name.ToLower()} ability");
    }

    protected abstract void ActivateAbility(Player player, Animator animator, AudioPlayer audioPlayer);
}