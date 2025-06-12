using RimWorld;
using System;
using Verse;

namespace AnomalyPatch.RitualDialogSorting
{
    public enum RitualSortProperty
    {
        PsychicSensitivity,
        AgeBiological
    }

    public static class RitualSortPropertyUtil
    {
        public static RitualSortProperty sortBy;
        public static bool reverse;

        public static string GetLabel(this RitualSortProperty property)
        {
            switch (property)
            {
                case RitualSortProperty.PsychicSensitivity: return StatDefOf.PsychicSensitivity.LabelCap;
                case RitualSortProperty.AgeBiological: return "AnomalyPatch_Age".Translate();
                default: throw new NotImplementedException();
            }
        }

        public static Func<Pawn, object> GetSortFunc(this RitualSortProperty property)
        {
            switch (property)
            {
                case RitualSortProperty.PsychicSensitivity: return p => p.GetStatValue(StatDefOf.PsychicSensitivity);
                case RitualSortProperty.AgeBiological: return p => p.ageTracker.AgeBiologicalTicks;
                default: throw new NotImplementedException();
            }
        }
    }
}
