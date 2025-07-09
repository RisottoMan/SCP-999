using System;
using System.Collections.Generic;
using System.Reflection;
using Exiled.API.Features;
using Scp999.Interfaces;

namespace Scp999.Features.Manager;
public static class AbilityManager
{
    private static List<IAbility> _abilityList = new();
    public static void RegisterAbilities()
    {
        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            try
            {
                if (type.IsInterface || type.IsAbstract || !type.GetInterfaces().Contains(typeof(IAbility)))
                    continue;

                var activator = Activator.CreateInstance(type) as IAbility;
                if (activator != null)
                {
                    _abilityList.Add(activator);
                    
                    Log.Debug($"Register the {activator.Name} ability.");
                    activator.Register();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in RegisterAbilities:" + ex.Message);
            }
        }
    }

    public static void UnregisterAbilities()
    {
        foreach (IAbility ability in _abilityList)
        {
            try
            {
                ability.Unregister();
            }
            catch (Exception ex)
            {
                Log.Error("Error in UnregisterAbilities:" + ex.Message);
            }
        }
    }

    public static List<IAbility> GetAbilities => _abilityList;
}