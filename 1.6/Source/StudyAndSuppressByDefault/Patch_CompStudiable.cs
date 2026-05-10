using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;

namespace AnomalyPatch.StudyAndSuppressByDefault
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.StudyAndSuppressByDefault)]
    [HarmonyPatch(typeof(CompStudiable))]
    [HarmonyPatch(nameof(CompStudiable.PostPostMake))]
    public static class Patch_CompStudiable
    {
        public static void Postfix(CompStudiable __instance)
        {
            if (Settings.StudyAndSuppressByDefault.Enabled() && __instance.Props.minMonolithLevelForStudy > 0)
            {
                __instance.SetStudyEnabled(true);
            }
        }
    }
}
