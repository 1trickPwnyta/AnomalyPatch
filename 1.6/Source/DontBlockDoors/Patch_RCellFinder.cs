using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;

namespace AnomalyPatch.DontBlockDoors
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.DontBlockDoors)]
    [HarmonyPatch(typeof(RCellFinder))]
    [HarmonyPatch("IsGoodDestinationFor")]
    public static class Patch_RCellFinder
    {
        public static void Postfix(IntVec3 c, Pawn pawn, bool careAboutDanger, ref bool __result)
        {
            if (Settings.DontBlockDoors.Enabled() && __result && careAboutDanger && pawn.IsPlayerControlled)
            {
                if (c.GetRegion(pawn.Map).IsDangerousDoorway() || c.HasTrap(pawn.Map))
                {
                    __result = false;
                }
            }
        }
    }
}
