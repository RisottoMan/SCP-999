using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class HealAbility : Ability
{
    public override string Name => "Heal";
    public override string Description => "Pressing the button activates the restoration of health around you for all players";
    public override int KeyId => 9993;
    public override KeyCode KeyCode => KeyCode.R;
    public override float Cooldown => 60f;
    protected override void ActivateAbility(Player player, Animator animator, AudioPlayer audioPlayer)
    {
        animator?.Play("HealthAnimation");
        audioPlayer.AddClip($"health");

        float distance = Plugin.Singleton.Config.MaxDistance;
        
        foreach (Player ply in Player.List)
        {
            if (player == ply)
                continue;
            
            if (Vector3.Distance(player.Position, ply.Position) < distance)
            {
                player.Heal(player.MaxHealth);
            }
        }
    }
    
    private IEnumerator<float> CheckEndOfAnimation(Player player, Animator animator)
    {
        yield return Timing.WaitForSeconds(0.1f);
        while (true)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("IdleAnimation"))
            {
                player.DisableEffect<Ensnared>();
                yield break;
            }
            
            yield return Timing.WaitForSeconds(0.5f);
        }
    }
}