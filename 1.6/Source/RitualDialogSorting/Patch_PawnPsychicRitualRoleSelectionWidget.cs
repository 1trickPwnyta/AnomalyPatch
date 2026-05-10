using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using Verse;
using Verse.AI.Group;

namespace AnomalyPatch.RitualDialogSorting
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.RitualDialogSorting)]
    [HarmonyPatch(typeof(PawnPsychicRitualRoleSelectionWidget))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new[] { typeof(PsychicRitualDef), typeof(PsychicRitualCandidatePool), typeof(PsychicRitualRoleAssignments) })]
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
