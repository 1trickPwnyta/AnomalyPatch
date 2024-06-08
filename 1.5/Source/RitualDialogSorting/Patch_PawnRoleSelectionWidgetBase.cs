using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnomalyPatch.RitualDialogSorting
{
    [HarmonyPatch(typeof(PawnRoleSelectionWidgetBase<PsychicRitualRoleDef>))]
    [HarmonyPatch("DrawRoleGroup_NewTemp")]
    public static class Patch_PawnRoleSelectionWidgetBase_DrawRoleGroup_NewTemp
    {
        public static void Prefix(PawnRoleSelectionWidgetBase<PsychicRitualRoleDef> __instance, ref IEnumerable<Pawn> selectedPawns)
        {
            if (__instance is PawnPsychicRitualRoleSelectionWidget)
            {
                selectedPawns = selectedPawns.OrderByDescending(p => p.GetStatValue(StatDefOf.PsychicSensitivity));
            }
        }
    }
}
