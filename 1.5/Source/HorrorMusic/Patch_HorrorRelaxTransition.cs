﻿using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace AnomalyPatch.HorrorMusic
{
    [HarmonyPatch(typeof(HorrorRelaxTransition))]
    [HarmonyPatch("IsValidPocketMap")]
    public static class Patch_HorrorRelaxTransition_IsValidPocketMap
    {
        public static void Postfix(PocketMapParent pocketMap, ref bool __result)
        {
            if (pocketMap.Map.generatorDef == MapGeneratorDefOf.Undercave)
            {
                __result = false;
            }
        }
    }

    [HarmonyPatch(typeof(HorrorRelaxTransition))]
    [HarmonyPatch("IsValidMap")]
    public static class Patch_HorrorRelaxTransition_IsValidMap
    {
        public static void Postfix(Map map, ref bool __result)
        {
            __result = map.listerThings.AnyThingWithDef(ThingDefOf.FleshmassHeart) || map.listerThings.AnyThingWithDef(ThingDefOf.Noctolith);
        }
    }
}
