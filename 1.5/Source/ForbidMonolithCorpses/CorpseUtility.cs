using RimWorld;
using Verse;

namespace AnomalyPatch.ForbidMonolithCorpses
{
    public static class CorpseUtility
    {
        public static void SetForbidden(Corpse corpse)
        {
            if (AnomalyPatchSettings.ForbidMonolithCorpses)
            {
                corpse.SetForbidden(true);
            }
        }
    }
}
