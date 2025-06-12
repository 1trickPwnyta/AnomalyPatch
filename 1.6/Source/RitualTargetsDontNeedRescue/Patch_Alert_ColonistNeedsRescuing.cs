using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace AnomalyPatch.RitualTargetsDontNeedRescue
{
    [HarmonyPatch(typeof(Alert_ColonistNeedsRescuing))]
    [HarmonyPatch(nameof(Alert_ColonistNeedsRescuing.NeedsRescue))]
    public static class Patch_Alert_ColonistNeedsRescuing
    {
        public static void Postfix(Pawn p, ref bool __result)
        {
            if (AnomalyPatchSettings.RitualTargetsDontNeedRescue)
            {
                Lord lord;
                if (p.TryGetLord(out lord) && lord.CurLordToil is LordToil_PsychicRitual)
                {
                    __result = false;
                }
            }
        }
    }
}
