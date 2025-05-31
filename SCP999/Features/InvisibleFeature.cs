using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace Scp999;
public class InvisibleFeature
{
    public static void MakeInvisibleForPlayer(Player player)
    {
        if (player.Role is FpcRole fpcRole) {
            fpcRole.IsInvisible = true;
        }
        /*
        player.ReferenceHub.transform.localScale = Vector3.zero;

        foreach (Player target in Player.List)
        {
            if (target == player)
                continue;

            Server.SendSpawnMessage?.Invoke(null, [player.ReferenceHub.networkIdentity, target.Connection]);
        }

        player.ReferenceHub.transform.localScale = scale;
        */
    }

    public static void RemoveInvisibleForPlayer(Player player)
    {
        if (player.Role is FpcRole fpcRole) {
            fpcRole.IsInvisible = false;
        }
    }
}