﻿using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Features.Abilities;
public class YippeeAbility : Ability
{
    public override string Name => "Yippee";
    public override string Description => "When you click on this button, you will play the <i>Yippee</i> sound that all players will hear";
    public override int KeyId => 9990;
    public override KeyCode KeyCode => KeyCode.Q;
    public override float Cooldown => 3f;
    protected override void ActivateAbility(Player player, Animator animator, AudioPlayer audioPlayer)
    {
        if (audioPlayer is null)
            return;
        
        // I would like a default yippee-tbh1.ogg to be used more often than yippee-tbh2.ogg
        int value = 1;
        int chance = Random.Range(0, 100);
        if (chance >= 60)
        {
            value = 2;
        }
        
        audioPlayer.AddClip($"yippee-tbh{value}");
    }
}