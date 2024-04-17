using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace AnomalyPatch.BioferriteHarvesterMultipleSelection
{
    [HarmonyPatch(typeof(RimWorld.InspectPaneUtility))]
    [HarmonyPatch(nameof(RimWorld.InspectPaneUtility.InspectPaneOnGUI))]
    public static class Patch_InspectPaneUtility_InspectPaneOnGUI
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_IInspectPane_get_ShouldShowPaneContents)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_1);
                    yield return new CodeInstruction(OpCodes.Call, AnomalyPatchRefs.m_InspectPaneUtility_DoPaneContentsForMultipleBioferriteHarvesters);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
