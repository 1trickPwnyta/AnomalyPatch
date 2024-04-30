using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPatch.StudyAndSuppressByDefault
{
    [HarmonyPatch(typeof(AnomalyUtility))]
    [HarmonyPatch(nameof(AnomalyUtility.MakeUnnaturalCorpse))]
    public static class Patch_AnomalyUtility_MakeUnnaturalCorpse
    {
        public static void Postfix(UnnaturalCorpse __result)
        {
            __result.Forbiddable.Forbidden = false;
            __result.GetComp<CompStudiable>().studyEnabled = true;
        }
    }
}
