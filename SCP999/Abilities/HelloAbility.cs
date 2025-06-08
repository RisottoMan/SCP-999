using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class HelloAbility : Ability
{
    public override string Name => "Hello";
    public override string Description => "Greeting players or attracting attention";
    public override int KeyId => 9992;
    public override KeyCode KeyCode => KeyCode.F;
    public override float Cooldown => 15f;
    protected override void ActivateAbility(Player player)
    {
        Animator animator = player.GameObject.GetComponent<PlayerComponent>().GetCurrentAnimator;
        if (animator is null)
            return;
        
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("IdleAnimation"))
            return;
        
        animator.Play("HelloAnimation");
    }
}