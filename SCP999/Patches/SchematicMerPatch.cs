using System.Diagnostics;
using HarmonyLib;

namespace Scp999.Patches;

[HarmonyPatch(typeof(ProjectMER.ProjectMER), nameof(ProjectMER.ProjectMER.SchematicsDir), MethodType.Getter)]
public class SchematicMerPatch
{
    public static bool Prefix(ref string __result)
    {
        var stackTrace = new StackTrace();
        foreach (var frame in stackTrace.GetFrames())
        {
            var declaringType = frame.GetMethod().DeclaringType;
            var assemblyName = declaringType.Assembly.GetName().Name;
            
            if (assemblyName == "Scp999" && declaringType.Name == "SchematicFeature")
            {
                __result = Plugin.Singleton.SchematicPath;
                return false;
            }
        }
        
        return true;
    }
}