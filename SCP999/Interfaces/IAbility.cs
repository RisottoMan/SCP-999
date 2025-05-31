namespace Scp999.Interfaces;
public interface IAbility
{
    string Name { get; }
    string Description { get; }
    int KeyId { get; }
    void Register();
    void Unregister();
}