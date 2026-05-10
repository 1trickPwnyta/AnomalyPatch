using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.DontHideStats
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.DontHideStats)]
    [HarmonyPatch(typeof(StatDrawEntry))]
    [HarmonyPatch(nameof(StatDrawEntry.ShouldDisplay))]
    public static class Patch_StatDrawEntry
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.LoadsField(typeof(ThingDef).Field(nameof(ThingDef.hideStats))))
                {
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_StatDrawEntry).Method(nameof(ShouldHideStats)));
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool ShouldHideStats(ThingDef def)
        {
            return def.hideStats && (!Settings.DontHideStats.Enabled() || (def != ThingDefOf.Metalhorror && def != ThingDef.Named("Revenant") && def != ThingDef.Named("Nociosphere")));
        }
    }
}
