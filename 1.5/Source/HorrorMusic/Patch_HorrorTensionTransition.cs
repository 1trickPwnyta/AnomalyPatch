using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace AnomalyPatch.HorrorMusic
{
    [HarmonyPatch(typeof(HorrorTensionTransition))]
    [HarmonyPatch(nameof(HorrorTensionTransition.IsTransitionSatisfied))]
    public static class Patch_HorrorTensionTransition_IsTransitionSatisfied
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_Find_get_Maps)
                {
                    instruction.operand = AnomalyPatchRefs.m_MapUtility_GetPlayerOccupiedMaps;
                }

                yield return instruction;
            }
        }
    }
}
