using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;

namespace AnomalyPatch.HoldingPlatformAlert
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.HoldingPlatformAlert)]
    [HarmonyPatch(typeof(Alert_NeedHoldingPlatform))]
    [HarmonyPatch(nameof(Alert_NeedHoldingPlatform.GetReport))]
    public static class Patch_Alert_NeedHoldingPlatform
    {
        public static void Postfix(ref AlertReport __result)
        {
            if (Settings.HoldingPlatformAlert.Enabled())
            {
                __result = false;
            }
        }
    }
}
