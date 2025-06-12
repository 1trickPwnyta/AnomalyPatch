using RimWorld;
using Verse;

namespace AnomalyPatch.NoProjectNoStudy
{
    public static class StudyUtility
    {
        public static bool ResearchSelectedForEntity(Thing entity)
        {
            CompStudiable comp = entity.TryGetComp<CompStudiable>();
            if (comp != null)
            {
                return Find.ResearchManager.CurrentAnomalyKnowledgeProjects.Any(project => (project.category == comp.KnowledgeCategory || project.category == comp.KnowledgeCategory.overflowCategory) && project.project != null);
            }
            return false;
        }

        public static bool StudyMakesProgressForEntity(Thing entity)
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
