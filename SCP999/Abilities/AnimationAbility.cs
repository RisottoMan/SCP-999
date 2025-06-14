using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class AnimationAbility : Ability
{
    public override string Name => "Dance";
    public override string Description => "Play a random funny animation. You will not be able to move during the animation";
    public override int KeyId => 9994;
    public override KeyCode KeyCode => KeyCode.T;
    public override float Cooldown => 5f;
    protected override void ActivateAbility(Player player, Animator animator, AudioPlayer audioPlayer)
    {
        int rand = Random.Range(0, 4) + 1;
        animator?.Play($"FunAnimation{rand}");
        //audioPlayer?.AddClip($"yippee-tbh1");
        player.EnableEffect<Ensnared>(30f);
        
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