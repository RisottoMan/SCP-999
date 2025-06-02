namespace Scp999.Interfaces;
public interface IAbility
{
    string Name { get; }
    string Description { get; }
    int KeyId { get; }
    int Cooldown { get; }
    void Register();
    void Unregister();
}