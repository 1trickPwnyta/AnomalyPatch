using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using SpecialSauce.Multipatch;
using Verse;

namespace AnomalyPatch.HorrorMusic
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.HorrorMusic)]
    [HarmonyPatch(typeof(HorrorRelaxTransition))]
    [HarmonyPatch("IsValidPocketMap")]
    public static class Patch_HorrorRelaxTransition_IsValidPocketMap
    {
        public static void Postfix(PocketMapParent pocketMap, ref bool __result)
        {
            if (Settings.HorrorMusic.Enabled() && pocketMap.Map.generatorDef == MapGeneratorDefOf.Undercave && !pocketMap.Map.mapPawns.AnyColonistSpawned)
            {
                __result = false;
            }
        }
    }

    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.HorrorMusic)]
    [HarmonyPatch(typeof(HorrorRelaxTransition))]
    [HarmonyPatch("IsValidMap")]
    public static class Patch_HorrorRelaxTransition_IsValidMap
    {
        public static void Postfix(Map map, ref bool __result)
        {
            if (Settings.HorrorMusic.Enabled())
            {
                __result = map.listerThings.AnyThingWithDef(ThingDefOf.FleshmassHeart) || map.listerThings.AnyThingWithDef(ThingDefOf.Noctolith);
            }
        }
    }
}
