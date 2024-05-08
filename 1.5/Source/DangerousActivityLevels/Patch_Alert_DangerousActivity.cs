using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace AnomalyPatch.DangerousActivityLevels
{
    [HarmonyPatch(typeof(Alert_DangerousActivity))]
    [HarmonyPatch(nameof(Alert_DangerousActivity.GetLabel))]
    public static class Patch_Alert_DangerousActivity_GetLabel
    {
        public static void Postfix(List<Thing> ___highActivity, ref string __result)
        {
            if (___highActivity.Count != 1)
            {
                __result += ": " + ___highActivity.MaxBy(thing => thing.TryGetComp<CompActivity>().ActivityLevel).TryGetComp<CompActivity>().ActivityLevel.ToStringPercent("0");
            }
        }
    }
}
