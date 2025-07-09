using System;
using Exiled.API.Features;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace Scp999.Features.Manager;
public static class SchematicManager
{
    public static SchematicObject AddSchematicByName(string schematicName)
    {
        try
        {
            return ObjectSpawner.SpawnSchematic(schematicName, Vector3.zero, Vector3.zero, Vector3.one);
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred when loading schematics: {ex}");
            return null;
        }
    }

    public static Animator GetAnimatorFromSchematic(SchematicObject schematicObject)
    {
        Animator animator = schematicObject?.GetComponentInChildren<Animator>(true);
        if (animator == null)
        {
            Log.Error("The animator was not found");
        }

        return animator;
    }
}