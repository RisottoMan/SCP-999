using System;
using System.Linq;
using Exiled.API.Features;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999.Features.Manager;
public class SchematicManager
{
    public static void AddSchematic(Player player, out SchematicObject schematicObject)
    {
        // Checking that the ProjectMER plugin is loaded on the server
        if (!AppDomain.CurrentDomain.GetAssemblies().Any(x => x.FullName.ToLower().Contains("projectmer")))
        {
            Log.Error("ProjectMER is not installed. Schematics can't spawn the SCP-999 game model.");
            schematicObject = null;
            return;
        }
        
        try
        {
            schematicObject = ObjectSpawner.SpawnSchematic("SCP999", player.Position, player.Rotation, Vector3.one);
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred when loading schematics: {ex}");
            schematicObject = null;
        }

        schematicObject.transform.parent = player.Transform;
        schematicObject.transform.rotation = new Quaternion();
        schematicObject.transform.position = player.Position + new Vector3(0, -0.75f, 0);

        player.Scale = new Vector3(0.00001f, 1, 0.00001f);
        schematicObject.transform.localScale = new Vector3(100000f, 1, 100000f);
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