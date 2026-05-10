using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.StopSuppression
{
    // Patched manually in mod initializer
    public static class Patch_JobDriver_ActivitySuppression
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundBreak = false;
            Label postActivityLevelCheckLabel = il.DefineLabel();
            LocalBuilder compActivity = il.DeclareLocal(typeof(CompActivity));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(typeof(ThingCompUtility).Method(nameof(ThingCompUtility.TryGetComp), new[] { typeof(Thing) }, new[] { typeof(CompActivity) })))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Stloc_S, compActivity);
                    continue;
                }
                if (instruction.Calls(typeof(CompActivity).PropertyGetter(nameof(CompActivity.ActivityLevel))))
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_JobDriver_ActivitySuppression).PropertyGetter(nameof(StopSuppressionEnabled)));
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

        private static bool StopSuppressionEnabled => Settings.StopSuppression.Enabled();
    }
}
