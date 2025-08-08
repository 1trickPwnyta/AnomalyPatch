using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPatch.DontBlockDoors
{
    [HarmonyPatch(typeof(RCellFinder))]
    [HarmonyPatch("IsGoodDestinationFor")]
    public static class Patch_RCellFinder
    {
        public static void Postfix(IntVec3 c, Pawn pawn, bool careAboutDanger, ref bool __result)
        {
            if (AnomalyPatchSettings.DontBlockDoors && __result && careAboutDanger && pawn.IsPlayerControlled)
            {
                if (c.GetRegion(pawn.Map).IsContainmentOrPrisonDoorway() || c.HasTrap(pawn.Map))
                {
                    __result = false;
                }
            }
        }
    }
}
