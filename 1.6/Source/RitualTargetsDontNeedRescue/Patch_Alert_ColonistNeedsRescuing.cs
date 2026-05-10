using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;
using Verse.AI.Group;

namespace AnomalyPatch.RitualTargetsDontNeedRescue
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.RitualTargetsDontNeedRescue)]
    [HarmonyPatch(typeof(Alert_ColonistNeedsRescuing))]
    [HarmonyPatch(nameof(Alert_ColonistNeedsRescuing.NeedsRescue))]
    public static class Patch_Alert_ColonistNeedsRescuing
    {
        public static void Postfix(Pawn p, ref bool __result)
        {
            if (Settings.RitualTargetsDontNeedRescue.Enabled())
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
