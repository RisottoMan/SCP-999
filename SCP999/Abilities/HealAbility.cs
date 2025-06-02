using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace Scp999.Abilities;
public class HealAbility : IAbility
{
    public string Name { get; } = "Heal";
    public string Description { get; } = "Restores health to players within a radius";
    public int KeyId { get; } = 9993;
    public int Cooldown { get; } = 15;
    public void Register()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived += KeybindActivateAbility;
    }

    public void Unregister()
    {
        ServerSpecificSettingsSync.ServerOnSettingValueReceived -= KeybindActivateAbility;
    }
    
    private void KeybindActivateAbility(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
    {
        if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != this.KeyId || !keybindSetting.SyncIsPressed)
            return;
        
        if (!Player.TryGet(referenceHub, out Player player))
            return;

        
        
        //Regardless of whether there is an animator or not to heal the players
        this.PlayAnimation(player);
        
        foreach (Player ply in Player.List)
        {
            if (player == ply)
                continue;
            
            if (Vector3.Distance(player.Position, ply.Position) < 5f)
            {
                player.Heal(100);
            }
        }
        
        Log.Debug("[HealAbility] Activating the heal ability");
    }

    private void PlayAnimation(Player player)
    {
        Animator animator = player.GameObject.GetComponent<PlayerComponent>().GetCurrentAnimator;
        if (animator is null)
            return;
        
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("IdleAnimation"))
            return;
        
        animator.Play("HealthAnimation");
    }
}