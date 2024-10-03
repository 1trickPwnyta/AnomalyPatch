using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace AnomalyPatch.StopSuppression
{
    // Patched manually in mod constructor
    public static class Patch_JobDriver_ActivitySuppression
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundBreak = false;
            Label postActivityLevelCheckLabel = il.DefineLabel();
            LocalBuilder compActivity = il.DeclareLocal(typeof(CompActivity));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_ThingCompUtility_TryGetComp)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stloc_S, compActivity);
                    continue;
                }
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_CompActivity_get_ActivityLevel)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Ldsfld, AnomalyPatchRefs.f_AnomalyPatch_Settings_StopSuppression);
                    yield return new CodeInstruction(OpCodes.Brtrue, postActivityLevelCheckLabel);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, compActivity);
                }
                if (instruction.opcode == OpCodes.Bge_Un_S)
                {
                    foundBreak = true;
                }
                if (foundBreak && instruction.opcode == OpCodes.Ldloc_0)
                {
                    instruction.labels.Add(postActivityLevelCheckLabel);
                }

                yield return instruction;
            }
        }
    }
}
