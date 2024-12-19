using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.CaravanDeathRefusal
{
    [HarmonyPatch(typeof(Caravan))]
    [HarmonyPatch(nameof(Caravan.Notify_MemberDied))]
    public static class Patch_Caravan
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundBrtrue = false;
            bool finished = false;

            Label destroyCaravanLabel = il.DefineLabel();

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundBrtrue && instruction.opcode == OpCodes.Brtrue)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_Caravan).Method(nameof(PatchUtility_Caravan.HasDeathRefusal)));
                    yield return new CodeInstruction(OpCodes.Ldsfld, typeof(AnomalyPatchSettings).Field(nameof(AnomalyPatchSettings.CaravanDeathRefusal)));
                    yield return new CodeInstruction(OpCodes.And);
                    yield return new CodeInstruction(OpCodes.Brfalse, destroyCaravanLabel);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_Caravan).Method(nameof(PatchUtility_Caravan.Free)));
                    yield return new CodeInstruction(OpCodes.Br, instruction.operand);
                    foundBrtrue = true;
                    continue;
                }
                if (foundBrtrue && !finished && instruction.opcode == OpCodes.Ldarg_0)
                {
                    instruction.labels.Add(destroyCaravanLabel);
                    finished = true;
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_Caravan
    {
        public static bool HasDeathRefusal(this Caravan caravan)
        {
            foreach (Corpse corpse in caravan.Goods.Where(t => t is Corpse))
            {
                if (DeathRefusalUtility.HasPlayerControlledDeathRefusal(corpse.InnerPawn))
                {
                    return true;
                }
            }
            return false;
        }

        public static void Free(this Caravan caravan, Pawn deadPawn)
        {
            caravan.pawns.Clear();
        }
    }
}
