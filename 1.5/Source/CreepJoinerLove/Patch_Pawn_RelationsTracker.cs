using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AnomalyPatch.CreepJoinerLove
{
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
                    yield return new CodeInstruction(OpCodes.Call, AnomalyPatchRefs.m_PawnDefUtilty_HasSameDefLabel);
                    yield return new CodeInstruction(OpCodes.Brfalse, instruction.operand);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
