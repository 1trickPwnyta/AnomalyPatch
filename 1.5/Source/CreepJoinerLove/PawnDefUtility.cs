using Verse;

namespace AnomalyPatch.CreepJoinerLove
{
    public static class PawnDefUtility
    {
        public static bool HasSameDefLabel(ThingDef a, ThingDef b)
        {
            if (AnomalyPatchSettings.CreepJoinerLove)
            {
                return a.label == b.label;
            }
            else
            {
                return a == b;
            }
        }
    }
}
