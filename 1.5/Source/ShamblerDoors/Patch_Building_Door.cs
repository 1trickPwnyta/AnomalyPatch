using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPatch.ShamblerDoors
{
    [HarmonyPatch(typeof(Building_Door))]
    [HarmonyPatch(nameof(Building_Door.CanPhysicallyPass))]
    public static class Patch_Building_Door
    {
        public static bool Prefix(Pawn p, Building_Door __instance, ref bool __result)
        {
            __result = (!ModsConfig.AnomalyActive || !p.IsMutant || p.mutant.Def.canOpenDoors || (AnomalyPatchSettings.ShamblerDoors && p.HostileTo(__instance))) && (__instance.FreePassage || __instance.PawnCanOpen(p) || (__instance.Open && p.HostileTo(__instance)));

            return false;
        }
    }
}
