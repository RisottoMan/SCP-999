using Exiled.API.Features;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Abilities;
public class HealAbility : Ability
{
    public override string Name => "Heal";
    public override string Description => "Pressing the button activates the restoration of health around you for all players";
    public override int KeyId => 9993;
    public override KeyCode KeyCode => KeyCode.R;
    public override float Cooldown => 5f; //60f; todo test
    protected override void ActivateAbility(Player player, Animator animator)
    {
        animator?.Play("HealthAnimation");

        float distance = Plugin.Singleton.Config.MaxDistance;
        
        foreach (Player ply in Player.List)
        {
            if (player == ply)
                continue;
            
            if (Vector3.Distance(player.Position, ply.Position) < distance)
            {
                player.Heal(player.MaxHealth);
            }
        }
    }
}