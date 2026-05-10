using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.CreepJoinerLove
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.CreepJoinerLove)]
    [HarmonyPatch(typeof(Pawn_RelationsTracker))]
    [HarmonyPatch(nameof(Pawn_RelationsTracker.SecondaryLovinChanceFactor))]
    public static class Patch_Pawn_RelationsTracker
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!finished && instruction.opcode == OpCodes.Bne_Un_S)
                {
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_Pawn_RelationsTracker).Method(nameof(HasSameDefLabel)));
                    yield return new CodeInstruction(OpCodes.Brfalse, instruction.operand);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool HasSameDefLabel(ThingDef a, ThingDef b)
        {
            if (Settings.CreepJoinerLove.Enabled())
            {
                return a.label == b.label;
            }
            else
            {
                return a == b;
            }
        }
    }
}
