using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.CaravanDeathRefusal
{
    [HarmonyPatch(typeof(ResurrectionUtility))]
    [HarmonyPatch(nameof(ResurrectionUtility.TryResurrect))]
    public static class Patch_ResurrectionUtility
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundSpawned = false;
            bool foundBlt = false;
            bool finished = false;

            LocalBuilder caravanLocal = il.DeclareLocal(typeof(Caravan));

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == typeof(Thing).Method("get_SpawnedOrAnyParentSpawned"))
                {
                    foundSpawned = true;
                }
                if (foundSpawned && instruction.opcode == OpCodes.Stloc_1)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Thing).Method("get_ParentHolder"));
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_ResurrectionUtility).Method(nameof(PatchUtility_ResurrectionUtility.GetCaravan)));
                    yield return new CodeInstruction(OpCodes.Stloc_S, caravanLocal);
                    continue;
                }
                if (instruction.opcode == OpCodes.Blt_S)
                {
                    foundBlt = true;
                }
                if (foundBlt && !finished && instruction.opcode == OpCodes.Ldarg_1)
                {
                    CodeInstruction newInstruction = new CodeInstruction(OpCodes.Ldarg_0);
                    newInstruction.labels.AddRange(instruction.labels);
                    yield return newInstruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_S, caravanLocal);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_ResurrectionUtility).Method(nameof(PatchUtility_ResurrectionUtility.ResurrectIntoCaravan)));
                    instruction.labels.Clear();
                    finished = true;
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_ResurrectionUtility
    {
        public static void ResurrectIntoCaravan(Pawn pawn, Caravan caravan)
        {
            if (caravan != null)
            {
                caravan.AddPawn(pawn, false);
                Find.WorldPawns.RemovePawn(pawn);
                Find.WorldPawns.PassToWorld(pawn, PawnDiscardDecideMode.Decide);
                if (pawn.apparel != null)
                {
                    foreach (Apparel apparel in pawn.apparel.WornApparel)
                    {
                       apparel.Notify_PawnResurrected(pawn);
                    }
                }
            }
        }

        public static Caravan GetCaravan(IThingHolder thingHolder)
        {
            if (thingHolder == null)
            {
                return null;
            }
            if (thingHolder is Caravan)
            {
                return thingHolder as Caravan;
            }
            return GetCaravan(thingHolder.ParentHolder);
        }
    }
}
