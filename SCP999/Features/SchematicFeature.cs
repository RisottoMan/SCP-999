using System;
using Exiled.API.Features;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using UnityEngine;

namespace Scp999;
public class SchematicFeature
{
    public static void AddSchematic(Player player, out SchematicObject schematicObject)
    {
        try
        {
            schematicObject = ObjectSpawner.SpawnSchematic("SCP999", Vector3.zero, Quaternion.identity, Vector3.one,null);
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred when loading schematics: {ex}");
            schematicObject = null;
            return;
        }
        
        schematicObject.transform.parent = player.Transform;
        schematicObject.transform.rotation = new Quaternion();
        schematicObject.transform.position = player.Position + new Vector3(0, -.25f, 0);
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