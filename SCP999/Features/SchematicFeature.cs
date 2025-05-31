using Exiled.API.Features;
using MapEditorReborn.API.Features.Objects;

namespace Scp999;
public class SchematicFeature
{
    public static void AddSchematic(Player player, out SchematicObject schematicObject)
    {
        //_schematicObject = MerExtensions.SpawnSchematicByName(SchematicName, player.Position, player.Rotation, player.Scale);
        //if (_schematicObject == null)
        //{
        //    this.RemoveRole(player);
        //    return;
        //}
        
        /*
        _animator = MerExtensions.GetAnimatorFromSchematic(_schematicObject);
        if (_animator == null)
        {
            this.RemoveRole(player);
            return;
        }*/
        
        //_schematicObject.transform.parent = player.Transform;
        //_schematicObject.transform.rotation = new Quaternion();
        //_schematicObject.transform.position = player.Position + new Vector3(0, -.25f, 0);
        schematicObject = new SchematicObject();
    }

    public static void RemoveSchematic(Player player, SchematicObject schematicObject)
    {
        schematicObject?.Destroy();
    }
}