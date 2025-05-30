using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace Scp999.Interfaces;
public interface IAbility
{
    string Name { get; }
    string Description { get; }
    KeyCode KeyBind { get; }
    void Register();
    void Unregister();
}