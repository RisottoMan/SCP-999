using Exiled.API.Features;
using Scp999.Interfaces;

namespace Scp999.Abilities;
public class FaceAbility // : Ability - todo in next updates
{
    public string Name => "Change face";
    public string Description => "Change the facial animation. You can become evil or kind";
    public int KeyId => 9995;
    public float Cooldown => 3f;
    protected void ActivateAbility(Player player)
    {
        //ChangeAnimation(); //todo
    }
}