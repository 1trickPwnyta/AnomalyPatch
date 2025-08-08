using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using Verse.AI;

namespace AnomalyPatch.DontBlockDoors
{
    [HarmonyPatch(typeof(Toils_Goto))]
    [HarmonyPatch(nameof(Toils_Goto.GotoThing))]
    [HarmonyPatch(new[] { typeof(TargetIndex), typeof(PathEndMode), typeof(bool) })]
    public static class Patch_Toils_Goto_GotoThing
    {
        public static void Postfix(TargetIndex ind, PathEndMode peMode, bool canGotoSpawnedParent, ref Toil __result)
        {
            if (AnomalyPatchSettings.DontBlockDoors && peMode == PathEndMode.Touch)
            {
                Toil toil = __result;
                toil.initAction = delegate ()
                {
                    Pawn actor = toil.actor;
                    LocalTargetInfo dest = actor.jobs.curJob.GetTarget(ind);
                    Thing thing = dest.Thing;
                    if (thing != null & canGotoSpawnedParent)
                    {
                        dest = thing.SpawnedParentOrMe;
                    }
                    DontBlockDoorsUtility.GotoBestCell(dest, actor, peMode);
                };
            }
        }
    }

    [HarmonyPatch(typeof(Toils_Goto))]
    [HarmonyPatch(nameof(Toils_Goto.GotoCell))]
    [HarmonyPatch(new[] { typeof(TargetIndex), typeof(PathEndMode) })]
    public static class Patch_Toils_Goto_GotoCell_TargetIndex
    {
        public static void Postfix(TargetIndex ind, PathEndMode peMode, ref Toil __result)
        {
            if (AnomalyPatchSettings.DontBlockDoors && peMode == PathEndMode.Touch)
            {
                Toil toil = __result;
                toil.initAction = delegate ()
                {
                    Pawn actor = toil.actor;
                    LocalTargetInfo target = actor.jobs.curJob.GetTarget(ind);
                    if (actor.Position == target.Cell)
                    {
                        actor.jobs.curDriver.ReadyForNextToil();
                        return;
                    }
                    DontBlockDoorsUtility.GotoBestCell(target, actor, peMode);
                };
            }
        }
    }

    [HarmonyPatch(typeof(Toils_Goto))]
    [HarmonyPatch(nameof(Toils_Goto.GotoCell))]
    [HarmonyPatch(new[] { typeof(IntVec3), typeof(PathEndMode) })]
    public static class Patch_Toils_Goto_GotoCell_IntVec3
    {
        public static void Postfix(IntVec3 cell, PathEndMode peMode, ref Toil __result)
        {
            if (AnomalyPatchSettings.DontBlockDoors && peMode == PathEndMode.Touch)
            {
                Toil toil = __result;
                toil.initAction = delegate ()
                {
                    Pawn actor = toil.actor;
                    if (actor.Position == cell)
                    {
                        actor.jobs.curDriver.ReadyForNextToil();
                        return;
                    }
                    DontBlockDoorsUtility.GotoBestCell(cell, actor, peMode);
                };
            }
        }
    }

    [HarmonyPatch]
    public static class PatchTargeted_Toils_Goto_GotoBuild_tickIntervalAction
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return typeof(Toils_Goto).GetNestedType("<>c__DisplayClass6_0", BindingFlags.NonPublic).Method("<GotoBuild>b__1");
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            CodeInstruction brFalse = instructionsList.FindLast(i => i.opcode == OpCodes.Brfalse_S);
            int index = instructionsList.IndexOf(brFalse);
            instructionsList.InsertRange(index + 1, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Call, typeof(DontBlockDoorsUtility).Method(nameof(DontBlockDoorsUtility.IsInContainmentOrPrisonDoorway))),
                new CodeInstruction(OpCodes.Brtrue_S, brFalse.operand),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Callvirt, typeof(Thing).PropertyGetter(nameof(Thing.Position))),
                new CodeInstruction(OpCodes.Ldloc_2),
                new CodeInstruction(OpCodes.Call, typeof(GenAdj).Method(nameof(GenAdj.IsInside), new[] { typeof(IntVec3), typeof(Thing) })),
                new CodeInstruction(OpCodes.Brtrue_S, brFalse.operand)
            });
            return instructionsList;
        }
    }
}
