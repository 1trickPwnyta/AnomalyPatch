using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPatch.InhumanPregnancyAttitude
{
    [HarmonyPatch(typeof(ThoughtWorker_PregnancyAttitude))]
    [HarmonyPatch("CurrentStateInternal")]
    public static class Patch_ThoughtWorker_PregnancyAttitude
    {
        public static void Postfix(Pawn p, ref ThoughtState __result)
        {
            if (AnomalyPatchSettings.InhumanPregnancyAttitude && p.Inhumanized())
            {
                __result = ThoughtState.Inactive;
            }
        }
    }
}
