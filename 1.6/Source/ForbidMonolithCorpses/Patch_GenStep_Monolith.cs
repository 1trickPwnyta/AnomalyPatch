using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.ForbidMonolithCorpses
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.ForbidMonolithCorpses)]
    [HarmonyPatch(typeof(GenStep_Monolith))]
    [HarmonyPatch(nameof(GenStep_Monolith.GenerateMonolith))]
    public static class Patch_GenStep_Monolith
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundMonolithSpawn = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundMonolithSpawn && instruction.Calls(typeof(GenSpawn).Method(nameof(GenSpawn.Spawn), new[] { typeof(Thing), typeof(IntVec3), typeof(Map), typeof(WipeMode) })))
                {
                    yield return instruction;
                    foundMonolithSpawn = true;
                    continue;
                }

                if (foundMonolithSpawn && instruction.Calls(typeof(GenSpawn).Method(nameof(GenSpawn.Spawn), new[] { typeof(Thing), typeof(IntVec3), typeof(Map), typeof(WipeMode) })))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 15);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_GenStep_Monolith).Method(nameof(SetForbidden)));
                    continue;
                }

                yield return instruction;
            }
        }

        private static void SetForbidden(Corpse corpse)
        {
            if (Settings.ForbidMonolithCorpses.Enabled())
            {
                corpse.SetForbidden(true);
            }
        }
    }
}
