using HarmonyLib;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.CharacterHighlighting
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.CharacterHighlighting)]
    [HarmonyPatch(typeof(SilhouetteUtility))]
    [HarmonyPatch("ShouldHighlightInt")]
    public static class Patch_SilhouetteUtility
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(typeof(ModsConfig).PropertyGetter(nameof(ModsConfig.AnomalyActive))))
                {
                    CodeInstruction newInstruction = new CodeInstruction(OpCodes.Call, typeof(Patch_SilhouetteUtility).PropertyGetter(nameof(CharacterHighlightingEnabled)));
                    newInstruction.labels.AddRange(instruction.labels);
                    yield return newInstruction;
                    yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                    yield return new CodeInstruction(OpCodes.Xor);
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool CharacterHighlightingEnabled => Settings.CharacterHighlighting.Enabled();
    }
}
