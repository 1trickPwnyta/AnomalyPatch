using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.AvoidDreadLeather
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.AvoidDreadLeather)]
    [HarmonyPatch(typeof(JobGiver_OptimizeApparel))]
    [HarmonyPatch(nameof(JobGiver_OptimizeApparel.ApparelScoreRaw))]
    public static class Patch_JobGiver_OptimizeApparel
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundHumanLeather = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldsfld && (FieldInfo)instruction.operand == typeof(ThoughtDefOf).Field(nameof(ThoughtDefOf.HumanLeatherApparelHappy)))
                {
                    foundHumanLeather = true;
                }
                if (foundHumanLeather && !finished && instruction.opcode == OpCodes.Ldarg_0)
                {
                    CodeInstruction newInstruction = new CodeInstruction(OpCodes.Ldloca_S, 0);
                    newInstruction.labels.AddRange(instruction.labels);
                    instruction.labels.Clear();
                    yield return newInstruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_JobGiver_OptimizeApparel).Method(nameof(PatchUtility_JobGiver_OptimizeApparel.ModifyApparelScore)));
                    finished = true;
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_JobGiver_OptimizeApparel
    {
        public static void ModifyApparelScore(ref float score, Pawn pawn, Apparel ap)
        {
            if (Settings.AvoidDreadLeather.Enabled() && ap.Stuff == ThingDefOf.Leather_Dread)
            {
                if (pawn == null || ThoughtUtility.CanGetThought(pawn, DefDatabase<ThoughtDef>.GetNamed("WearingDreadLeather"), true))
                {
                    score -= 0.5f;
                    if (score > 0f)
                    {
                        score *= 0.1f;
                    }
                }
            }
        }
    }
}
