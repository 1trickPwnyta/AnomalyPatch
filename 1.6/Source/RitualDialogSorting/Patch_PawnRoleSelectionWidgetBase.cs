using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AnomalyPatch.RitualDialogSorting
{
    [HarmonyPatch(typeof(PawnRoleSelectionWidgetBase<PsychicRitualRoleDef>))]
    [HarmonyPatch("DrawRoleGroup")]
    public static class Patch_PawnRoleSelectionWidgetBase_DrawRoleGroup
    {
        public static void Prefix(PawnRoleSelectionWidgetBase<PsychicRitualRoleDef> __instance, ref IEnumerable<Pawn> selectedPawns)
        {
            if (AnomalyPatchSettings.RitualDialogSorting && __instance is PawnPsychicRitualRoleSelectionWidget)
            {
                IOrderedEnumerable<Pawn> sortedPawns = selectedPawns.OrderByDescending(p => !p.Downed && p.IsFreeNonSlaveColonist);
                Func<Pawn, object> sortFunc = RitualSortPropertyUtil.sortBy.GetSortFunc();
                if (RitualSortPropertyUtil.reverse)
                {
                    selectedPawns = sortedPawns.ThenByDescending(sortFunc);
                } 
                else
                {
                    selectedPawns = sortedPawns.ThenBy(sortFunc);
                }
            }
        }
    }

    [HarmonyPatch(typeof(PawnRoleSelectionWidgetBase<PsychicRitualRoleDef>))]
    [HarmonyPatch(nameof(PawnRoleSelectionWidgetBase<PsychicRitualRoleDef>.DrawPawnList))]
    public static class Patch_PawnRoleSelectionWidgetBase_DrawPawnList
    {
        public static void Prefix(PawnRoleSelectionWidgetBase<PsychicRitualRoleDef> __instance, ref Rect listRectPawns)
        {
            if (AnomalyPatchSettings.RitualDialogSorting && __instance is PawnPsychicRitualRoleSelectionWidget)
            {
                float widgetHeight = 24f;
                Rect labelRect = new Rect(listRectPawns.x, listRectPawns.y, listRectPawns.width * 2 / 10, widgetHeight);
                Rect buttonRect = new Rect(listRectPawns.x + labelRect.width, listRectPawns.y, listRectPawns.width * 5 / 10, widgetHeight);
                Rect checkboxRect = new Rect(listRectPawns.x + labelRect.width + buttonRect.width, listRectPawns.y, listRectPawns.width * 3 / 10, widgetHeight);
                listRectPawns.yMin += buttonRect.height + 8f;
                Widgets.Label(labelRect, "AnomalyPatch_SortBy".Translate());
                if (Widgets.ButtonText(buttonRect, RitualSortPropertyUtil.sortBy.GetLabel()))
                {
                    List<FloatMenuOption> options = new List<FloatMenuOption>();
                    foreach (RitualSortProperty property in typeof(RitualSortProperty).GetEnumValues())
                    {
                        options.Add(new FloatMenuOption(property.GetLabel(), delegate ()
                        {
                            RitualSortPropertyUtil.sortBy = property;
                        }));
                    }
                    Find.WindowStack.Add(new FloatMenu(options));
                }
                Widgets.CheckboxLabeled(checkboxRect, "   " + "AnomalyPatch_Reverse".Translate(), ref RitualSortPropertyUtil.reverse);
            }
        }
    }
}
