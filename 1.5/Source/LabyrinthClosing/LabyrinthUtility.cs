using RimWorld;
using Verse;

namespace AnomalyPatch.LabyrinthClosing
{
    public static class LabyrinthUtility
    {
        public static void ForbidIfOutsideHomeZone(Thing thing)
        {
            if (!thing.Map.areaManager.Home[thing.Position])
            {
                thing.SetForbidden(true);
            }
        }
    }
}
