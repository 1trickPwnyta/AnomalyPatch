using HarmonyLib;
using RimWorld.QuestGen;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.UnnaturalDarknessMapFix
{
    [HarmonyPatch(typeof(QuestNode_Root_UnnaturalDarkness))]
    [HarmonyPatch("TestRunInt")]
    public static class Patch_QuestNode_Root_UnnaturalDarkness_TestRunInt
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == typeof(QuestGen_Get).Method(nameof(QuestGen_Get.GetMap)))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    instruction.operand = typeof(PatchUtility_QuestNode_Root_UnnaturalDarkness).Method(nameof(PatchUtility_QuestNode_Root_UnnaturalDarkness.GetMap));
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(QuestNode_Root_UnnaturalDarkness))]
    [HarmonyPatch("RunInt")]
    public static class Patch_QuestNode_Root_UnnaturalDarkness_RunInt
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == typeof(QuestGen_Get).Method(nameof(QuestGen_Get.GetMap)))
                {
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(QuestGen).Field(nameof(QuestGen.slate)));
                    instruction.operand = typeof(PatchUtility_QuestNode_Root_UnnaturalDarkness).Method(nameof(PatchUtility_QuestNode_Root_UnnaturalDarkness.GetMap));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_QuestNode_Root_UnnaturalDarkness
    {
        public static Map GetMap(bool mustBeInfestable, int? preferMapWithMinFreeColonists, Slate slate)
        {
            return slate.Get<Map>("map") ?? QuestGen_Get.GetMap(mustBeInfestable, preferMapWithMinFreeColonists);
        }
    }
}
