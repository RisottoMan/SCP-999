using Exiled.API.Features;
using Scp999.Interfaces;

namespace Scp999.Abilities;
public class SizeAbility : Ability
{
    public override string Name => "Size";
    public override string Description => "Change the size of a specific player";
    public override int KeyId => 9991;
    public override float Cooldown => 30f;
    protected override void ActivateAbility(Player player)
    {
        //todo
        //Raycast
        //ChangeSize
    }
}