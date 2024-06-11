using HarmonyLib;
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
}
