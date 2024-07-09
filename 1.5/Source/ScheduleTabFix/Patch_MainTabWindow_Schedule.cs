using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnomalyPatch.ScheduleTabFix
{
    [HarmonyPatch(typeof(MainTabWindow_Schedule))]
    [HarmonyPatch("get_Pawns")]
    public static class Patch_MainTabWindow_Schedule
    {
        public static void Postfix(ref IEnumerable<Pawn> __result)
        {
            if (AnomalyPatchSettings.ScheduleTabFix)
            {
                __result = __result.Where(p => p.timetable != null || (p.playerSettings != null && p.playerSettings.SupportsAllowedAreas));
            }
        }
    }
}
