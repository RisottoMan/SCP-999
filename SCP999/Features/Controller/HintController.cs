using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Features;
using Scp999.Features.Manager;
using Scp999.Interfaces;
using UnityEngine;

namespace Scp999.Features.Controller;
public class HintController : MonoBehaviour
{
    public void Init(Player player)
    {
        _player = player;
        _abilities = AbilityManager.GetAbilities.OrderBy(r => r.KeyId).ToList();
        _controller = player.GameObject.GetComponent<CooldownController>();
        InvokeRepeating(nameof(CheckHint), 0f, 0.5f);
        Log.Debug($"[CooldownController] Invoke the hint cycle");
    }
    
    void CheckHint()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<align=right>");
        stringBuilder.Append("<size=50><color=#ffa500>\ud83d\ude06 <b>SCP-999</b></color></size>\n");
        stringBuilder.Append("Abilities:\n");
        
        foreach (var ability in _abilities)
        {
            string color = "#ffa500";
            if (!_controller.IsAbilityAvailable(ability.Name))
            {
                color = "#966100";
            }
                    
            stringBuilder.Append($"<color={color}>{ability.Name}  [{ability.KeyCode}]</color>\n");
        }
                
        stringBuilder.Append($"\n<size=18>if you cant use abilities\nremove \u2b50 in settings</size>");
        stringBuilder.Append("</align>\n\n\n\n\n\n\n");
        _player.ShowHint(stringBuilder.ToString(), 1f);
    }
    
    void OnDestroy()
    {
        CancelInvoke(nameof(CheckHint));
        Log.Debug($"[CooldownController] Cancel the hint cycle");
    }

    private Player _player;
    private List<IAbility> _abilities;
    private CooldownController _controller;
}