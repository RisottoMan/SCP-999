using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class YippeeAbility : Ability
{
    public override string Name => "Yippee";
    public override string Description => "Play the Yippee sound";
    public override int KeyId => 9990;
    public override float Cooldown => 3f;
    protected override void ActivateAbility(Player player)
    {
        AudioPlayer audioPlayer = player.GameObject.GetComponent<PlayerComponent>().GetCurrentAudioPlayer;
        if (audioPlayer is null)
            return;

        audioPlayer.AddClip($"yippee-tbh{Random.Range(0, 2)}");
    }
}