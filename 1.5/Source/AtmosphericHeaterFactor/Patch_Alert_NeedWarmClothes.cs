using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AnomalyPatch.AtmosphericHeaterFactor
{
    [HarmonyPatch(typeof(Alert_NeedWarmClothes))]
    [HarmonyPatch("MapWithMissingWarmClothes")]
    public static class Patch_Alert_NeedWarmClothes_MapWithMissingWarmClothes
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundCheck = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundCheck && instruction.opcode == OpCodes.Brfalse_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    yield return new CodeInstruction(OpCodes.Call, AnomalyPatchRefs.m_AtmosphericHeaterUtility_DisableNeedWarmClothesAlert);
                    yield return new CodeInstruction(OpCodes.Brtrue_S, instruction.operand);
                    foundCheck = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
