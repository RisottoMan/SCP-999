using Exiled.API.Features;
using Scp999.Interfaces;

namespace Scp999.Abilities;
public class SizeAbility // : Ability - todo in next updates
{
    public string Name => "Size";
    public string Description => "Change the size of a specific player";
    public int KeyId => 9991;
    public float Cooldown => 30f;
    protected void ActivateAbility(Player player)
    {
        //todo
        //Raycast
        //ChangeSize
    }
}