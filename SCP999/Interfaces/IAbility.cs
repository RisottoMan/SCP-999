using UnityEngine;

namespace Scp999.Interfaces;
public interface IAbility
{
    string Name { get; }
    string Description { get; }
    int KeyId { get; }
    KeyCode KeyCode { get; } 
    float Cooldown { get; }
    void Register();
    void Unregister();
}