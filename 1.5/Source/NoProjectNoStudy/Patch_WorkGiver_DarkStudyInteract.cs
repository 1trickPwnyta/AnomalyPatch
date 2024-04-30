using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace AnomalyPatch.NoProjectNoStudy
{
    [HarmonyPatch(typeof(WorkGiver_DarkStudyInteract))]
    [HarmonyPatch(nameof(WorkGiver_DarkStudyInteract.HasJobOnThing))]
    public static class Patch_WorkGiver_DarkStudyInteract_HasJobOnThing
    {
        public static void Postfix(Thing t, ref bool __result)
        {
            if (__result)
            {
                Thing entity = t is Building_HoldingPlatform ? ((Building_HoldingPlatform)t).HeldPawn : t;
                if (!StudyUtility.ResearchSelectedForEntity(entity) && !StudyUtility.StudyMakesProgressForEntity(entity))
                {
                    JobFailReason.Is("AnomalyPatch_NoResearchSelected".Translate());
                    __result = false;
                }
            }
        }
    }
}
