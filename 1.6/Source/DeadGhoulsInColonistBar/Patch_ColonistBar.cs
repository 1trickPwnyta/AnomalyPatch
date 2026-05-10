using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.DeadGhoulsInColonistBar
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.DeadGhoulsInColonistBar)]
    [HarmonyPatch(typeof(ColonistBar))]
    [HarmonyPatch("CheckRecacheEntries")]
    public static class Patch_ColonistBar
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundIsColonist1 = false;
            bool foundBrfalse1 = false;
            bool addedLabel1 = false;
            bool foundIsColonist2 = false;
            bool foundBrfalse2 = false;
            bool addedLabel2 = false;
            Label addCorpseLabel1 = il.DefineLabel();
            Label addCorpseLabel2 = il.DefineLabel();

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundIsColonist1 && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == typeof(Pawn).PropertyGetter(nameof(Pawn.IsColonist)))
                {
                    foundIsColonist1 = true;
                    continue;
                }
                if (foundIsColonist1 && !foundBrfalse1 && instruction.opcode == OpCodes.Brfalse_S)
                {
                    instruction.opcode = OpCodes.Br_S;
                    foundBrfalse1 = true;
                }
                if (foundBrfalse1 && !addedLabel1 && instruction.opcode == OpCodes.Ldsfld)
                {
                    instruction.labels.Add(addCorpseLabel1);
                    addedLabel1 = true;
                }
                if (foundIsColonist1 && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == typeof(Pawn).PropertyGetter(nameof(Pawn.IsColonist)))
                {
                    foundIsColonist2 = true;
                }
                if (foundIsColonist2 && !foundBrfalse2 && instruction.opcode == OpCodes.Brfalse_S)
                {
                    instruction.opcode = OpCodes.Br_S;
                    foundBrfalse2 = true;
                }
                if (foundBrfalse2 && !addedLabel2 && instruction.opcode == OpCodes.Ldsfld)
                {
                    instruction.labels.Add(addCorpseLabel2);
                    addedLabel2 = true;
                }
            }

            bool foundIsColonist3 = false;
            bool foundIsColonist4 = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundIsColonist3 && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == typeof(Pawn).PropertyGetter(nameof(Pawn.IsColonist)))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Brtrue_S, addCorpseLabel1);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 5);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_ColonistBar).Method(nameof(PatchUtility_ColonistBar.ShouldShowGhoul)));
                    yield return new CodeInstruction(OpCodes.Brtrue_S, addCorpseLabel1);
                    foundIsColonist3 = true;
                    continue;
                }
                if (foundIsColonist3 && !foundIsColonist4 && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == typeof(Pawn).PropertyGetter(nameof(Pawn.IsColonist)))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Brtrue_S, addCorpseLabel2);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 7);
                    yield return new CodeInstruction(OpCodes.Callvirt, typeof(Corpse).PropertyGetter(nameof(Corpse.InnerPawn)));
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_ColonistBar).Method(nameof(PatchUtility_ColonistBar.ShouldShowGhoul)));
                    yield return new CodeInstruction(OpCodes.Brtrue_S, addCorpseLabel2);
                    foundIsColonist4 = true;
                    continue;
                }
                if (instruction.opcode == OpCodes.Callvirt && instruction.operand is MethodInfo info && info == typeof(Pawn).PropertyGetter(nameof(Pawn.IsColonySubhumanPlayerControlled)))
                {
                    instruction.operand = typeof(PatchUtility_ColonistBar).Method(nameof(PatchUtility_ColonistBar.ShouldShowGhoul));
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_ColonistBar
    {
        public static bool ShouldShowGhoul(Pawn p)
        {
            return Settings.DeadGhoulsInColonistBar.Enabled() && p.IsColonySubhuman;
        }
    }
}
