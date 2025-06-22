using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Features.Abilities;
public class AnimationAbility : Ability
{
    public override string Name => "Dance";
    public override string Description => "Play a random funny animation. You will not be able to move during the animation";
    public override int KeyId => 9994;
    public override KeyCode KeyCode => KeyCode.T;
    public override float Cooldown => 15f;
    protected override void ActivateAbility(Player player, Animator animator, AudioPlayer audioPlayer)
    {
        player.EnableEffect<Ensnared>(30f);
        
        int rand = Random.Range(0, 100) + 1;
        switch (rand)
        {
            // throwing balls
            case > 0 and <= 15:
            {
                animator?.Play($"FunAnimation1");
                audioPlayer?.AddClip($"circus"); 
            } break;
            
            // Jump x3
            case > 15 and <= 60:
            {
                animator?.Play($"FunAnimation2");
                audioPlayer?.AddClip($"jump"); 
            } break;
            
            // Shrinking
            case > 60 and <= 90:
            {
                animator?.Play($"FunAnimation3");
                audioPlayer?.AddClip($"funnytoy"); 
            } break;
            
            // UwU - Secret animation
            case > 90:
            {
                animator?.Play($"FunAnimation4");
                audioPlayer?.AddClip($"uwu"); 
            } break;
        }
        
        Timing.RunCoroutine(this.CheckEndOfAnimation(player, animator));
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