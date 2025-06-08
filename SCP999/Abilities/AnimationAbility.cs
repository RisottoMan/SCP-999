using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class AnimationAbility : Ability
{
    public override string Name => "Dance";
    public override string Description => "Play a random funny animation";
    public override int KeyId => 9994;
    public override KeyCode KeyCode => KeyCode.T;
    public override float Cooldown => 5f;
    protected override void ActivateAbility(Player player)
    {
        Animator animator = player.GameObject.GetComponent<PlayerComponent>().GetCurrentAnimator;
        if (animator is null)
            return;
        
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("IdleAnimation"))
            return;
        
        animator.Play($"FunAnimation_{Random.Range(0, 5)}");
    }
}