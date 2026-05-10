using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;

namespace AnomalyPatch.ExtractBioferriteByDefault
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.ExtractBioferriteByDefault)]
    [HarmonyPatch(typeof(CompHoldingPlatformTarget))]
    [HarmonyPatch(nameof(CompHoldingPlatformTarget.Notify_HeldOnPlatform))]
    public static class Patch_CompHoldingPlatformTarget
    {
        public static void Postfix(CompHoldingPlatformTarget __instance)
        {
            if (Settings.ExtractBioferriteByDefault.Enabled() && ResearchProjectDefOf.BioferriteExtraction.IsFinished && !ResearchProjectDef.Named("BioferriteHarvesting").IsFinished)
            {
                __instance.extractBioferrite = true;
            }
        }
    }
}
