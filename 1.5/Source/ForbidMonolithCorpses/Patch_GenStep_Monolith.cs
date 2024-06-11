using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace AnomalyPatch.ForbidMonolithCorpses
{
    [HarmonyPatch(typeof(GenStep_Monolith))]
    [HarmonyPatch(nameof(GenStep_Monolith.GenerateMonolith))]
    public static class Patch_GenStep_Monolith
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundMonolithSpawn = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundMonolithSpawn && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_GenSpawn_Spawn)
                {
                    yield return instruction;
                    foundMonolithSpawn = true;
                    continue;
                }

                if (foundMonolithSpawn && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_GenSpawn_Spawn)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 15);
                    yield return new CodeInstruction(OpCodes.Call, AnomalyPatchRefs.m_CorpseUtility_SetForbidden);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
