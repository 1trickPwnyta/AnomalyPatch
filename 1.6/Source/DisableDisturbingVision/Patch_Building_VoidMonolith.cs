using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;

namespace AnomalyPatch.DisableDisturbingVision
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.DisableDisturbingVision)]
    [HarmonyPatch(typeof(Building_VoidMonolith))]
    [HarmonyPatch(nameof(Building_VoidMonolith.SpawnSetup))]
    public static class Patch_Building_VoidMonolith
    {
        public static void Postfix(ref int ___disturbingVisionTick)
        {
            if (Settings.DisableDisturbingVision.Enabled())
            {
                ___disturbingVisionTick = -99999;
            }
        }
    }
}
