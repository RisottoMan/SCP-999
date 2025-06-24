using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace Scp999.Features.Manager;
public static class InvisibleManager
{
    /// <summary>
    /// Make a specific SCP-999 invisible to all players
    /// </summary>
    /// <param name="scp999">A player with the role of SCP-999</param>
    public static void MakeInvisible(Player scp999)
    {
        foreach (Player other in Player.List)
        {
            if (scp999 == other)
                continue;
            
            if (scp999.Role.Is(out FpcRole fpc))
            {
                fpc.IsInvisibleFor.Add(other);
            }
        }
    }
    
    /// <summary>
    /// Make a specific SCP-999 invisible for a specific player
    /// </summary>
    /// <param name="scp999">A player with the role of SCP-999</param>
    /// <param name="player">The player who shouldn't see SCP-999</param>
    public static void MakeInvisibleForPlayer(Player scp999, Player player)
    {
        if (scp999.Role.Is(out FpcRole fpc))
        {
            fpc.IsInvisibleFor.Add(player);
        }
    }

    /// <summary>
    /// Remove the invisibility of a specific SCP-999 for all players
    /// </summary>
    /// <param name="scp999">A player with the role of SCP-999</param>
    public static void RemoveInvisible(Player scp999)
    {
        if (scp999.Role.Is(out FpcRole fpc))
        {
            foreach (Player player in fpc.IsInvisibleFor)
            {
                fpc.IsInvisibleFor.Remove(player);
            }
        }
    }
    
    /// <summary>
    /// Remove the invisibility of a specific SCP-999 for a specific player
    /// </summary>
    /// <param name="scp999">A player with the role of SCP-999</param>
    /// <param name="player">The player who should see SCP-999</param>
    public static void RemoveInvisibleForPlayer(Player scp999, Player player)
    {
        if (scp999.Role.Is(out FpcRole fpc))
        {
            fpc.IsInvisibleFor.Remove(player);
        }
    }
}