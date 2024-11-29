using Exiled.Events.EventArgs.Player;

namespace SCP999.Interfaces;
public interface IAbility
{
    string Name { get; }
    string Description { get;  }
    ItemType ItemType { get; }
    void Invoke(UsingItemEventArgs ev);
}