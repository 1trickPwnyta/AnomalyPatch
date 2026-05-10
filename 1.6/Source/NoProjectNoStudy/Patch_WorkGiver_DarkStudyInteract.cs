using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;
using Verse.AI;

namespace AnomalyPatch.NoProjectNoStudy
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.NoProjectNoStudy)]
    [HarmonyPatch(typeof(WorkGiver_DarkStudyInteract))]
    [HarmonyPatch(nameof(WorkGiver_DarkStudyInteract.HasJobOnThing))]
    public static class Patch_WorkGiver_DarkStudyInteract_HasJobOnThing
    {
        public static void Postfix(Thing t, ref bool __result)
        {
            if (Settings.NoProjectNoStudy.Enabled() && __result)
            {
                Thing entity = t is Building_HoldingPlatform ? ((Building_HoldingPlatform)t).HeldPawn : t;
                if (!ResearchSelectedForEntity(entity) && !StudyMakesProgressForEntity(entity))
                {
                    JobFailReason.Is("AnomalyPatch_NoResearchSelected".Translate());
                    __result = false;
                }
            }
        }

        private static bool ResearchSelectedForEntity(Thing entity)
        {
            CompStudiable comp = entity.TryGetComp<CompStudiable>();
            if (comp != null)
            {
                return Find.ResearchManager.CurrentAnomalyKnowledgeProjects.Any(project => (project.category == comp.KnowledgeCategory || project.category == comp.KnowledgeCategory.overflowCategory) && project.project != null);
            }
            return false;
        }

        private static bool StudyMakesProgressForEntity(Thing entity)
        {
            CompStudiable comp = entity.TryGetComp<CompStudiable>();
            if (comp != null)
            {
                CompStudyUnlocks compStudyUnlocks = comp.parent.TryGetComp<CompStudyUnlocks>();
                if (compStudyUnlocks != null)
                {
                    return !compStudyUnlocks.Completed;
                }
            }

            if (entity is UnnaturalCorpse)
            {
                return !((UnnaturalCorpse)entity).Tracker.CanDestroyViaResearch;
            }

            return false;
        }
    }
}
