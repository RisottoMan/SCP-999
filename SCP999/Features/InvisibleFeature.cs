using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace Scp999;
public class InvisibleFeature
{
    public static void MakeInvisible(Player player)
    {
        foreach (Player other in Player.List)
        {
            if (player == other)
                continue;
            
            if (player.Role.Is(out FpcRole fpc))
            {
                fpc.IsInvisibleFor.Add(other);
            }
        }
    }
    
    public static void MakeInvisibleForPlayer(Player scp999, Player player)
    {
        if (scp999.Role.Is(out FpcRole fpc))
        {
            fpc.IsInvisibleFor.Add(player);
        }
    }

    public static void RemoveInvisible(Player player)
    {
        foreach (Player other in Player.List)
        {
            if (player == other)
                continue;
            
            if (player.Role.Is(out FpcRole fpc))
            {
                fpc.IsInvisibleFor.Remove(other);
            }
        }
    }
    
    public static void RemoveInvisibleForPlayer(Player scp999, Player player)
    {
        if (scp999.Role.Is(out FpcRole fpc))
        {
            fpc.IsInvisibleFor.Remove(player);
        }
    }
}