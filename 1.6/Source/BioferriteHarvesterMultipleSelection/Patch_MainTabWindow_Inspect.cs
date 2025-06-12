using HarmonyLib;
using RimWorld;
using UnityEngine;

namespace AnomalyPatch.BioferriteHarvesterMultipleSelection
{
    [HarmonyPatch(typeof(MainTabWindow_Inspect))]
    [HarmonyPatch("get_ShouldShowPaneContents")]
    public static class Patch_MainTabWindow_Inspect_get_ShouldShowPaneContents
    {
        public static void Postfix(ref bool __result)
        {
            __result |= InspectPaneUtility.ShouldShowContentsForMultipleBioferriteHarvesters();
        }
    }

    [HarmonyPatch(typeof(MainTabWindow_Inspect))]
    [HarmonyPatch(nameof(MainTabWindow_Inspect.DoPaneContents))]
    public static class Patch_MainTabWindow_Inspect_DoPaneContents
    {
        public static bool Prefix(Rect rect)
        {
            if (InspectPaneUtility.ShouldShowContentsForMultipleBioferriteHarvesters())
            {
                InspectPaneUtility.DoPaneContentsForMultipleBioferriteHarvesters(rect);
                return false;
            }

            return true;
        }
    }
}
