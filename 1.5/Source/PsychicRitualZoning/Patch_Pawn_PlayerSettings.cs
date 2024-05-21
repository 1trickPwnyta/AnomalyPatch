using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace AnomalyPatch.PsychicRitualZoning
{
    [HarmonyPatch(typeof(Pawn_PlayerSettings))]
    [HarmonyPatch("get_RespectsAllowedArea")]
    public static class Patch_Pawn_PlayerSettings
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundLord = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundLord && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_LordUtility_GetLord)
                {
                    foundLord = true;
                }
                if (foundLord && !finished && instruction.opcode == OpCodes.Brfalse_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, AnomalyPatchRefs.f_Pawn_PlayerSettings_pawn);
                    yield return new CodeInstruction(OpCodes.Call, AnomalyPatchRefs.m_LordUtility_GetLord);
                    yield return new CodeInstruction(OpCodes.Call, AnomalyPatchRefs.m_LordUtility_IsPsychicRitual);
                    yield return new CodeInstruction(OpCodes.Brtrue_S, instruction.operand);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
