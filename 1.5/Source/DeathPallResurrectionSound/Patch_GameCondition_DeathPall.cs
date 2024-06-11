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
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            Label silentInputLabel = il.DefineLabel();
            Label nopLabel = il.DefineLabel();

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldsfld && (FieldInfo)instruction.operand == AnomalyPatchRefs.f_MessageTypeDefOf_NegativeEvent)
                {
                    yield return new CodeInstruction(OpCodes.Ldsfld, AnomalyPatchRefs.f_AnomalyPatchSettings_DeathPallResurrectionSound);
                    yield return new CodeInstruction(OpCodes.Brtrue_S, silentInputLabel);
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Br_S, nopLabel);
                    CodeInstruction silentInputInstruction = new CodeInstruction(OpCodes.Ldsfld, AnomalyPatchRefs.f_MessageTypeDefOf_SilentInput);
                    silentInputInstruction.labels.Add(silentInputLabel);
                    yield return silentInputInstruction;
                    CodeInstruction nopInstruction = new CodeInstruction(OpCodes.Nop);
                    nopInstruction.labels.Add(nopLabel);
                    yield return nopInstruction;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
