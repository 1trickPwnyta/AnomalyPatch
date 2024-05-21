using RimWorld;
using Verse.AI.Group;

namespace AnomalyPatch.PsychicRitualZoning
{
    public static class LordUtility
    {
        public static bool IsPsychicRitual(Lord lord)
        {
            return lord != null && lord.LordJob is LordJob_PsychicRitual;
        }
    }
}
