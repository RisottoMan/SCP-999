using System;
using Exiled.API.Features;
using MEC;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999.Features.Manager;
public class SchematicManager
{
    public static void AddSchematic(Player player, out SchematicObject schematicObject)
    {
        try
        {
            schematicObject = ObjectSpawner.SpawnSchematic("SCP999", Vector3.zero, player.Rotation, Vector3.one);
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred when loading schematics: {ex}");
            schematicObject = null;
            return;
        }

        schematicObject.transform.parent = player.Transform;
        schematicObject.transform.position = player.Position + new Vector3(0, -0.75f, 0);
    }

    public static void ChangeSize(Player player, SchematicObject schematicObject)
    {
        Timing.CallDelayed(0.1f, () =>
        {
            player.Scale = new Vector3(0.00001f, 1, 0.00001f);
            schematicObject.transform.localScale = new Vector3(100000f, 1, 100000f);
        });
    }
    
    public static void RemoveSchematic(SchematicObject schematicObject)
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