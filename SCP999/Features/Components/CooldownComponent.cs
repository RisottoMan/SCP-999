using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scp999;
public class CooldownComponent : MonoBehaviour
{
    void Awake()
    {
        this._abilityCooldown = AbilityFeature.GetAbilities.ToDictionary(item => item.Name, item => 0f);
        InvokeRepeating(nameof(CheckCooldown), 0f, 1f);
    }

    void CheckCooldown()
    {
        foreach (var key in this._abilityCooldown.Keys.ToList())
        {
            if (this._abilityCooldown[key] > 0)
            {
                this._abilityCooldown[key]--;
            }
            else
            {
                this._abilityCooldown[key] = 0;
            }
        }
    }
    
    void OnDestroy()
    {
        CancelInvoke(nameof(CheckCooldown));
    }

    public bool IsAbilityAvailable(string ability)
    {
        return this._abilityCooldown[ability] <= 0;
    }

    public void SetCooldownForAbility(string ability, float time)
    {
        this._abilityCooldown[ability] = time;
    }
    
    private Dictionary<string, float> _abilityCooldown;
}