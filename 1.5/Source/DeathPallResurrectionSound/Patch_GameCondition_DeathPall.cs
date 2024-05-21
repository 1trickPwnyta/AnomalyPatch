using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace AnomalyPatch.DeathPallResurrectionSound
{
    [HarmonyPatch(typeof(GameCondition_DeathPall))]
    [HarmonyPatch(nameof(GameCondition_DeathPall.GameConditionTick))]
    public static class Patch_GameCondition_DeathPall
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldsfld && (FieldInfo)instruction.operand == AnomalyPatchRefs.f_MessageTypeDefOf_NegativeEvent)
                {
                    instruction.operand = AnomalyPatchRefs.f_MessageTypeDefOf_SilentInput;
                }

                yield return instruction;
            }
        }
    }
}
