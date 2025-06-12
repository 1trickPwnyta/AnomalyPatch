using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace AnomalyPatch.FoodPriority
{
    [HarmonyPatch(typeof(CaravanPawnsNeedsUtility))]
    [HarmonyPatch(nameof(CaravanPawnsNeedsUtility.GetFoodScore))]
    [HarmonyPatch(new[] { typeof(ThingDef), typeof(Pawn), typeof(float) })]
    public static class Patch_CaravanPawnsNeedsUtility_GetFoodScore
    {
        public static void Postfix(ThingDef food, Pawn pawn, ref float __result)
        {
            if (AnomalyPatchSettings.FoodPriority)
            {
                if (!pawn.RaceProps.Humanlike || pawn.IsGhoul)
                {
                    if (food == ThingDefOf.Meat_Twisted)
                    {
                        __result += 0.2f;
                    }
                    else if (food == ThingDefOf.Meat_Human || food == ThingDef.Named("Meat_Megaspider"))
                    {
                        __result += 0.1f;
                    }
                }
            }
        }
    }
}
