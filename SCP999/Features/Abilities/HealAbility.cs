using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class HealAbility : Ability
{
    public override string Name => "Heal";
    public override string Description => "Pressing the button activates the restoration of health around you for all players";
    public override int KeyId => 9993;
    public override KeyCode KeyCode => KeyCode.R;
    public override float Cooldown => 60f;
    protected override void ActivateAbility(Player player, Animator animator, AudioPlayer audioPlayer)
    {
        animator?.Play("HealthAnimation");
        audioPlayer.AddClip($"health");
        
        Timing.RunCoroutine(this.CheckEndOfAnimation(player));
    }

    private IEnumerator<float> CheckEndOfAnimation(Player player)
    {
        float distance = Plugin.Singleton.Config.MaxDistance;
        int counter = _healPlayerTimes;
        
        while (counter > 0)
        {
            foreach (Player ply in Player.List)
            {
                if (player == ply)
                    continue;
            
                if (Vector3.Distance(player.Position, ply.Position) < distance)
                {
                    float value;
                    if (player.MaxHealth > 2000)
                    {
                        value = player.MaxHealth / 20; //SCP
                    }
                    else
                    {
                        value = player.MaxHealth / 5; //Human
                    }
                    
                    ply.Heal(value);
                }
            }

            counter--;
            yield return Timing.WaitForSeconds(1f);
        }
    }
    
    private int _healPlayerTimes = 5;
}