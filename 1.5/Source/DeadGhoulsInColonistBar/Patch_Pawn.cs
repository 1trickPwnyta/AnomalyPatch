using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace AnomalyPatch.DeadGhoulsInColonistBar
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("get_IsColonyMutantPlayerControlled")]
    public static class Patch_Pawn
    {
        public static void Postfix(Pawn __instance, ref bool __result)
        {
            if (AnomalyPatchSettings.DeadGhoulsInColonistBar && !__result && __instance.IsColonyMutant && __instance.mutant.Def.canBeDrafted && (__instance.CarriedBy != null || __instance.GetCaravan() != null))
            {
                __result = true;
            }
        }
    }
}
