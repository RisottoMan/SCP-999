using System.Diagnostics;
using System.IO;
using HarmonyLib;

namespace Scp999.Patches;

[HarmonyPatch(typeof(MapEditorReborn.MapEditorReborn), nameof(MapEditorReborn.MapEditorReborn.SchematicsDir), MethodType.Getter)]
public class SchematicMerPatch
{
    public static bool Prefix(ref string __result)
    {
        var stackTrace = new StackTrace();
        foreach (var frame in stackTrace.GetFrames())
        {
            var declaringType = frame.GetMethod().DeclaringType;
            var assemblyName = declaringType.Assembly.GetName().Name;

            if (assemblyName == "SCP999" && declaringType.Name == "Features")
            {
                __result = Path.Combine(Plugin.SchematicPath, "Schematics");
                return false;
            }
        }
        
        return true;
    }
}