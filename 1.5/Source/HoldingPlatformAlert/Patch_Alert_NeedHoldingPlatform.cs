using HarmonyLib;
using RimWorld;

namespace AnomalyPatch.HoldingPlatformAlert
{
    [HarmonyPatch(typeof(Alert_NeedHoldingPlatform))]
    [HarmonyPatch(nameof(Alert_NeedHoldingPlatform.GetReport))]
    public static class Patch_Alert_NeedHoldingPlatform
    {
        public static void Postfix(ref AlertReport __result)
        {
            if (AnomalyPatchSettings.HoldingPlatformAlert)
            {
                __result = false;
            }
        }
    }
}
