using Exiled.API.Features;
using RoleAPI.API.Interfaces;
using RoleAPI.API.Managers;
using UnityEngine;

namespace Scp999.Features.Abilities;
public class YippeeAbility : Ability
{
    public override string Name => "Yippee";
    public override string Description => "Play just a funny Yippee sound";
    public override int KeyId => 9990;
    public override KeyCode KeyCode => KeyCode.Q;
    public override float Cooldown => 3f;
    protected override bool ActivateAbility(Player player, ObjectManager manager)
    {
        if (manager.AudioPlayer is null)
            return false;
        
        // I would like a default yippee-tbh1.ogg to be used more often than yippee-tbh2.ogg
        int value = 1;
        int chance = Random.Range(0, 100);
        if (chance >= 60)
        {
            value = 2;
        }
        
        manager.AudioPlayer.AddClip($"yippee-tbh{value}");
        return true;
    }
}