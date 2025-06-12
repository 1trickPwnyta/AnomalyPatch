using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace AnomalyPatch.DontHideStats
{
    [HarmonyPatch(typeof(StatDrawEntry))]
    [HarmonyPatch(nameof(StatDrawEntry.ShouldDisplay))]
    public static class Patch_StatDrawEntry
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == AnomalyPatchRefs.f_ThingDef_hideStats)
                {
                    yield return new CodeInstruction(OpCodes.Call, AnomalyPatchRefs.m_StatUtility_ShouldHideStats);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
