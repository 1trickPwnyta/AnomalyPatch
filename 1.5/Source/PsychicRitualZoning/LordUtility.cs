using RimWorld;
using Verse.AI.Group;

namespace AnomalyPatch.PsychicRitualZoning
{
    public static class LordUtility
    {
        public static bool LordRespectsAllowedArea(Lord lord)
        {
            return AnomalyPatchSettings.PsychicRitualZoning && lord != null && lord.LordJob is LordJob_PsychicRitual;
        }
    }
}
