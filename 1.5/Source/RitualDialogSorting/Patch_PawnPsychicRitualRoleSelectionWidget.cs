using RimWorld;
using Verse;

namespace AnomalyPatch.RitualDialogSorting
{
    // Patched manually in mod constructor
    public static class Patch_PawnPsychicRitualRoleSelectionWidget
    {
        public static void Postfix(PsychicRitualDef ritualDef)
        {
            if (ritualDef == DefDatabase<PsychicRitualDef>.GetNamed("Chronophagy"))
            {
                RitualSortPropertyUtil.sortBy = RitualSortProperty.AgeBiological;
                RitualSortPropertyUtil.reverse = true;
            }
            else
            {
                RitualSortPropertyUtil.sortBy = RitualSortProperty.PsychicSensitivity;
                RitualSortPropertyUtil.reverse = true;
            }
        }
    }
}
