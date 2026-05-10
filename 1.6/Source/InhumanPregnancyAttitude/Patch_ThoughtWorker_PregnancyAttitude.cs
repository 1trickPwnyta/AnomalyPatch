using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using Verse;

namespace AnomalyPatch.InhumanPregnancyAttitude
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.InhumanPregnancyAttitude)]
    [HarmonyPatch(typeof(ThoughtWorker_PregnancyAttitude))]
    [HarmonyPatch("CurrentStateInternal")]
    public static class Patch_ThoughtWorker_PregnancyAttitude
    {
        public static void Postfix(Pawn p, ref ThoughtState __result)
        {
            if (Settings.InhumanPregnancyAttitude.Enabled() && p.Inhumanized())
            {
                __result = ThoughtState.Inactive;
            }
        }
    }
}
