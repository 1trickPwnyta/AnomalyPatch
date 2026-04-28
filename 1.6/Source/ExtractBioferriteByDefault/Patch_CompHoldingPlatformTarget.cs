using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPatch.ExtractBioferriteByDefault
{
    [HarmonyPatch(typeof(CompHoldingPlatformTarget))]
    [HarmonyPatch(nameof(CompHoldingPlatformTarget.Notify_HeldOnPlatform))]
    public static class Patch_CompHoldingPlatformTarget
    {
        public static void Postfix(CompHoldingPlatformTarget __instance)
        {
            if (AnomalyPatchSettings.ExtractBioferriteByDefault && ResearchProjectDefOf.BioferriteExtraction.IsFinished && !ResearchProjectDef.Named("BioferriteHarvesting").IsFinished)
            {
                __instance.extractBioferrite = true;
            }
        }
    }
}
