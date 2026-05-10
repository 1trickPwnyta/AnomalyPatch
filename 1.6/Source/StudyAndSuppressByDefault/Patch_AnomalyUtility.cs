using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using Verse;

namespace AnomalyPatch.StudyAndSuppressByDefault
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.StudyAndSuppressByDefault)]
    [HarmonyPatch(typeof(AnomalyUtility))]
    [HarmonyPatch(nameof(AnomalyUtility.MakeUnnaturalCorpse))]
    public static class Patch_AnomalyUtility_MakeUnnaturalCorpse
    {
        public static void Postfix(UnnaturalCorpse __result)
        {
            if (Settings.StudyAndSuppressByDefault.Enabled())
            {
                __result.Forbiddable.Forbidden = false;
                __result.GetComp<CompStudiable>().studyEnabled = true;
            }
        }
    }
}
