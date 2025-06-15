using System;
using Exiled.API.Features;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999;
public class SchematicFeature
{
    public static void AddSchematic(Player player, out SchematicObject schematicObject)
    {
        try
        {
            schematicObject = ObjectSpawner.SpawnSchematic("SCP999", player.Position, player.Rotation, Vector3.one);
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred when loading schematics: {ex}");
            schematicObject = null;
        }

        /*
        schematicObject.transform.parent = player.Transform;
        schematicObject.transform.rotation = new Quaternion();
        schematicObject.transform.position = player.Position + new Vector3(0, -0.75f, 0);
        */
    }

    public static void RemoveSchematic(Player player, SchematicObject schematicObject)
    {
        schematicObject?.Destroy();
    }

    public static void GetAnimatorFromSchematic(SchematicObject schematicObject, out Animator animator)
    {
        animator = schematicObject?.GetComponentInChildren<Animator>(true);
        if (animator == null)
        {
            Log.Error("The animator was not found");
        }
    }
}