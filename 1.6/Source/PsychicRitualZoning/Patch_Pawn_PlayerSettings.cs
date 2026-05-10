using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;
using Verse.AI.Group;

namespace AnomalyPatch.PsychicRitualZoning
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.PsychicRitualZoning)]
    [HarmonyPatch(typeof(Pawn_PlayerSettings))]
    [HarmonyPatch(nameof(Pawn_PlayerSettings.RespectsAllowedArea))]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_Pawn_PlayerSettings
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundLord = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundLord && instruction.Calls(typeof(LordUtility).Method(nameof(LordUtility.GetLord), new[] { typeof(Pawn) })))
                {
                    foundLord = true;
                }
                if (foundLord && !finished && instruction.opcode == OpCodes.Brfalse_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(Pawn_PlayerSettings).Field("pawn"));
                    yield return new CodeInstruction(OpCodes.Call, typeof(LordUtility).Method(nameof(LordUtility.GetLord), new[] { typeof(Pawn) }));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_Pawn_PlayerSettings).Method(nameof(LordRespectsAllowedArea)));
                    yield return new CodeInstruction(OpCodes.Brtrue_S, instruction.operand);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool LordRespectsAllowedArea(Lord lord)
        {
            return Settings.PsychicRitualZoning.Enabled() && lord != null && lord.LordJob is LordJob_PsychicRitual;
        }
    }
}
