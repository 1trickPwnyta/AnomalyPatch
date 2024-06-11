using RimWorld;
using Verse;

namespace AnomalyPatch.DontHideStats
{
    public static class StatUtility
    {
        public static bool ShouldHideStats(ThingDef def)
        {
            return def.hideStats && (!AnomalyPatchSettings.DontHideStats || (def != ThingDefOf.Metalhorror && def != ThingDef.Named("Revenant") && def != ThingDef.Named("Nociosphere")));
        }
    }
}
