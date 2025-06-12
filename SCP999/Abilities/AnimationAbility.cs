using Exiled.API.Features;
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
    protected override void ActivateAbility(Player player, Animator animator)
    {
        int rand = Random.Range(0, 4) + 1;
        animator?.Play($"FunAnimation{rand}");
    }
}