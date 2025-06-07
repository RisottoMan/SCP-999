using Exiled.API.Features;
using Scp999.Interfaces;

namespace Scp999.Abilities;
public class FaceAbility : Ability
{
    public override string Name => "Face";
    public override string Description => "Change the facial animation";
    public override int KeyId => 9995;
    public override float Cooldown => 3f;
    protected override void ActivateAbility(Player player)
    {
        //ChangeAnimation(); //todo
    }
}