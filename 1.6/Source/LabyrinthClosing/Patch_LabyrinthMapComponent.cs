using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.LabyrinthClosing
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.LabyrinthClosing)]
    [HarmonyPatch(typeof(LabyrinthMapComponent))]
    [HarmonyPatch("TeleportPawnsClosing")]
    public static class Patch_LabyrinthMapComponent_TeleportPawnsClosing
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundThings = false;
            bool foundSkip = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundThings && instruction.LoadsField(typeof(Map).Field(nameof(Map.spawnedThings))))
                {
                    foundThings = true;
                }
                if (foundThings && !foundSkip && instruction.Calls(typeof(SkipUtility).Method(nameof(SkipUtility.SkipTo))))
                {
                    foundSkip = true;
                }
                if (foundSkip && !finished && instruction.opcode == OpCodes.Pop)
                {
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_LabyrinthMapComponent_TeleportPawnsClosing).Method(nameof(ForbidIfOutsideHomeZone)));
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }

        private static void ForbidIfOutsideHomeZone(Thing thing)
        {
            if (Settings.LabyrinthClosing.Enabled())
            {
                thing.SetForbiddenIfOutsideHomeArea();
            }
        }
    }
}
