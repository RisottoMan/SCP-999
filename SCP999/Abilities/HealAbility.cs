using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class HealAbility : Ability
{
    public override string Name => "Heal";
    public override string Description => "Restores health to players within a radius";
    public override int KeyId => 9993;
    public override float Cooldown => 15f;

    protected override void ActivateAbility(Player player)
    {
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