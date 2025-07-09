using LabApi.Features.Wrappers;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999.Features.Manager;
public static class TextToyManager
{
    public static TextToy CreateTextForSchematic(Player scp066, SchematicObject schematicObject)
    {
        TextToy textToyObject = TextToy.Create(Vector3.zero, Quaternion.identity, Vector3.one, null, false);
        textToyObject.TextFormat = "<color=#ffa500>SCP-999</color>";
        textToyObject.Parent = schematicObject.transform;
        textToyObject.Transform.localPosition += new Vector3(0, 1, 0);
        textToyObject.Transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        textToyObject.Transform.localScale = Vector3.one * 0.2f;
        textToyObject.Spawn();

        /*
        // Spawn TextToy for all players except SCP-066
        foreach (Player player in Player.List)
        {
            if (player == scp066)
                continue;

            NetworkServer.SendSpawnMessage(textToyObject.Base.netIdentity, player.Connection);
        }
        */
            
        return textToyObject;
    }
}