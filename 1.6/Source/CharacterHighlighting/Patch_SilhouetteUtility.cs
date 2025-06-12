using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.CharacterHighlighting
{
    [HarmonyPatch(typeof(SilhouetteUtility))]
    [HarmonyPatch("ShouldHighlightInt")]
    public static class Patch_SilhouetteUtility
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_ModsConfig_get_AnomalyActive)
                {
                    CodeInstruction newInstruction = new CodeInstruction(OpCodes.Ldsfld, AnomalyPatchRefs.f_AnomalyPatchSettings_CharacterHighlighting);
                    newInstruction.labels.AddRange(instruction.labels);
                    yield return newInstruction;
                    yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                    yield return new CodeInstruction(OpCodes.Xor);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
