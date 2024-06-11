using RimWorld;
using Verse;

namespace AnomalyPatch.LabyrinthClosing
{
    public static class LabyrinthUtility
    {
        public static void ForbidIfOutsideHomeZone(Thing thing)
        {
            if (AnomalyPatchSettings.LabyrinthClosing)
            {
                thing.SetForbiddenIfOutsideHomeArea();
            }
        }
    }
}
