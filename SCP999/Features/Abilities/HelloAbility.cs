using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;
using RoleAPI.API;
using RoleAPI.API.Interfaces;
using UnityEngine;

namespace Scp999.Features.Abilities;
public class HelloAbility : Ability
{
    public override string Name => "Hello";
    public override string Description => "When you click on this button, you will greet the players or ask for attention";
    public override int KeyId => 9992;
    public override KeyCode KeyCode => KeyCode.F;
    public override float Cooldown => 15f;
    protected override void ActivateAbility(Player player, ExtendedRole role)
    {
        player.EnableEffect<Ensnared>(3f);
        role.Animator?.Play("HelloAnimation");

        if (Random.Range(0, 2) == 0)
        {
            role.AudioPlayer.AddClip("hello");
        }
        else
        {
            role.AudioPlayer.AddClip("hi");
        }
        
        Timing.RunCoroutine(this.CheckEndOfAnimation(player, role.Animator));
    }
    
    private IEnumerator<float> CheckEndOfAnimation(Player player, Animator animator)
    {
        yield return Timing.WaitForSeconds(0.1f);
        while (true)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("HelloAnimation"))
            {
                player.DisableEffect<Ensnared>();
                yield break;
            }
            
            yield return Timing.WaitForSeconds(0.3f);
        }
    }
}