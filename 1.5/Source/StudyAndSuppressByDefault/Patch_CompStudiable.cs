using HarmonyLib;
using RimWorld;

namespace AnomalyPatch.StudyAndSuppressByDefault
{
    [HarmonyPatch(typeof(CompStudiable))]
    [HarmonyPatch(nameof(CompStudiable.PostPostMake))]
    public static class Patch_CompStudiable
    {
        public static void Postfix(CompStudiable __instance)
        {
            if (AnomalyPatchSettings.StudyAndSuppressByDefault && __instance.Props.minMonolithLevelForStudy > 0)
            {
                __instance.SetStudyEnabled(true);
            }
        }
    }
}
