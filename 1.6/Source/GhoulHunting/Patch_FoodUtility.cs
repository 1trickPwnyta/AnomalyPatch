using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Reflection;
using Verse;

namespace AnomalyPatch.GhoulHunting
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.GhoulHunting)]
    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch(nameof(FoodUtility.TryFindBestFoodSourceFor))]
    public static class Patch_FoodUtility_TryFindBestFoodSourceFor
    {
        public static void Postfix(Pawn getter, Pawn eater, ref Thing foodSource, ref ThingDef foodDef, bool forceScanWholeMap, ref bool __result)
        {
            if (Settings.GhoulHunting.Enabled())
            {
                if (!__result && getter == eater && eater.IsGhoul)
                {
                    Pawn pawn = (Pawn)typeof(FoodUtility).GetMethod("BestPawnToHuntForPredator", BindingFlags.Static | BindingFlags.NonPublic, null, new[] { typeof(Pawn), typeof(bool) }, null).Invoke(null, new object[] { getter, forceScanWholeMap });
                    if (pawn != null)
                    {
                        foodSource = pawn;
                        foodDef = FoodUtility.GetFinalIngestibleDef(foodSource, false);
                        __result = true;
                    }
                }
            }
        }
    }

    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.GhoulHunting)]
    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch(nameof(FoodUtility.IsAcceptablePreyFor))]
    public static class Patch_FoodUtility_IsAcceptablePreyFor
    {
        public static void Postfix(Pawn predator, Pawn prey, ref bool __result)
        {
            if (__result && predator.IsGhoul && (PawnUtility.GetManhunterOnDamageChance(prey, predator) > 0f || prey.HostileTo(predator)) && !prey.Downed)
            {
                __result = false;
            }
        }
    }
}
