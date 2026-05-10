using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AnomalyPatch.DeathPallResurrectionSound
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.DeathPallResurrectionSound)]
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
                if (instruction.LoadsField(typeof(MessageTypeDefOf).Field(nameof(MessageTypeDefOf.NegativeEvent))))
                {
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_GameCondition_DeathPall).PropertyGetter(nameof(DeathPallResurrectionSoundEnabled)));
                    yield return new CodeInstruction(OpCodes.Brtrue_S, silentInputLabel);
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Br_S, nopLabel);
                    CodeInstruction silentInputInstruction = new CodeInstruction(OpCodes.Ldsfld, typeof(MessageTypeDefOf).Field(nameof(MessageTypeDefOf.SilentInput)));
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

        private static bool DeathPallResurrectionSoundEnabled => Settings.DeathPallResurrectionSound.Enabled();
    }
}
