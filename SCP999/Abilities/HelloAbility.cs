using CustomPlayerEffects;
using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class HelloAbility : Ability
{
    public override string Name => "Hello";
    public override string Description => "When you click on this button, you will greet the players or ask for attention";
    public override int KeyId => 9992;
    public override KeyCode KeyCode => KeyCode.F;
    public override float Cooldown => 15f;
    protected override void ActivateAbility(Player player, Animator animator, AudioPlayer audioPlayer)
    {
        animator?.Play("HelloAnimation");
        player.EnableEffect<Ensnared>(3f);
        //audioPlayer.AddClip($"hello");
    }
}