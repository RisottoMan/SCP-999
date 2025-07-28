using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;
using RoleAPI.API.Interfaces;
using RoleAPI.API.Managers;
using UnityEngine;

namespace Scp999.Features.Abilities;
public class HelloAbility : Ability
{
    public override string Name => "Hello";
    public override string Description => "Greet the players and wave your paw";
    public override int KeyId => 9992;
    public override KeyCode KeyCode => KeyCode.F;
    public override float Cooldown => 15f;
    protected override void ActivateAbility(Player player, ObjectManager manager)
    {
        player.EnableEffect<Ensnared>(3f);
        manager.Animator?.Play("HelloAnimation");

        if (Random.Range(0, 2) == 0)
        {
            manager.AudioPlayer.AddClip("hello");
        }
        else
        {
            manager.AudioPlayer.AddClip("hi");
        }
        
        Timing.RunCoroutine(this.CheckEndOfAnimation(player, manager.Animator));
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