using HarmonyLib;
using RimWorld;

namespace AnomalyPatch.DisableDisturbingVision
{
    [HarmonyPatch(typeof(Building_VoidMonolith))]
    [HarmonyPatch(nameof(Building_VoidMonolith.SpawnSetup))]
    public static class Patch_Building_VoidMonolith
    {
        public static void Postfix(ref int ___disturbingVisionTick)
        {
            if (AnomalyPatchSettings.DisableDisturbingVision)
            {
                ___disturbingVisionTick = -99999;
            }
        }
    }
}
