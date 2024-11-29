using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using SCP999.Interfaces;
using UnityEngine;

namespace SCP999;
public class Heal : IAbility
{
    public string Name { get; } = "Heal";
    public string Description { get; } = "Restores health to players within a radius";
    public ItemType ItemType { get; } = ItemType.SCP207;
    private float _amount { get; } = 40;
    private float _timing { get; } = 0.25f;

    public void Invoke(UsingItemEventArgs ev)
    {
        int count = 0;
        foreach (Player victim in Player.List)
        {
            if (Vector3.Distance(ev.Player.Position, victim.Position) < Plugin.Singleton.Config.MaxDistance && victim != ev.Player)
            {
                count++;

                ev.Player.Broadcast(5, "Test broadcast"); // PlayerBroadcast
                victim.ShowHint("Test hint", 5); // VictimBroadcast

                Timing.RunCoroutine(HealCoroutine(ev.Player));
            }
        }
    }

    public IEnumerator<float> HealCoroutine(Player scp999)
    {
        for (int i = 0; i <= _amount;  i++)
        {
            if (scp999 is null)
            {
                break;
            }

            foreach (Player player in Player.List)
            {
                if (Vector3.Distance(scp999.Position, player.Position) > Plugin.Singleton.Config.MaxDistance || player == scp999)
                {
                    continue;
                }

                player.EnableEffect(EffectType.Stained, _timing, true);
                player.Heal(1);
            }
            yield return Timing.WaitForSeconds(_timing);
        }
    }
}