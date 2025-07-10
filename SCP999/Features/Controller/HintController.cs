using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Features;
using Hints;
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
        
        int i = 0;
        foreach (var ability in _abilities)
        {
            string color = "#ffa500";
            if (!_controller.IsAbilityAvailable(ability.Name))
            {
                color = "#966100";
            }
            
            stringBuilder.Append($"<color={color}>{ability.Name}  {{{i}}}</color>\n");
            i++;
        }
                
        stringBuilder.Append($"\n<size=18>if you cant use abilities\nremove \u2b50 in settings</size>");
        stringBuilder.Append("</align>\n\n\n\n\n\n\n");
        _player.HintDisplay.Show(new TextHint(stringBuilder.ToString(), [
            new SSKeybindHintParameter(9990),
            new SSKeybindHintParameter(9991),
            new SSKeybindHintParameter(9992),
            new SSKeybindHintParameter(9993),
        ], durationScalar: 1f));
    }
    
    /* Цвет может быть как светлым (активная способность), так и темным (пассив)
       При этом тут есть ещё и SSKeybindHintParameter с номером из каждой Ability
       Также я вынес текст каждой способности в строку, чтобы была возможность поменять
       Осталось понять как обуздать эту монструозную конструкцию и превратить в абстракт
    void CheckHint()
    {
        string text = "<align=right><size=50><color=#ffa500>\ud83d\ude06 <b>SCP-999</b></color></size>\n" +
                      "Abilities:\n" +
                      "<color={color}>Yippee  {0}</color>\n" +
                      "<color={color}>Hello  {1}</color>\n" +
                      "<color={color}>Heal  {2}</color>\n" +
                      "<color={color}>Dance  {3}</color>\n" +
                      "\n<size=18>if you cant use abilities\nremove \u2b50 in settings</size></align>\n\n\n\n\n\n\n";
        
        _player.HintDisplay.Show(new TextHint(text, [
            new SSKeybindHintParameter(9990),
            new SSKeybindHintParameter(9991),
            new SSKeybindHintParameter(9992),
            new SSKeybindHintParameter(9993),
        ], durationScalar: 1f));
    }*/
    
    void OnDestroy()
    {
        CancelInvoke(nameof(CheckHint));
        Log.Debug($"[CooldownController] Cancel the hint cycle");
    }

    private Player _player;
    private List<IAbility> _abilities;
    private CooldownController _controller;
}