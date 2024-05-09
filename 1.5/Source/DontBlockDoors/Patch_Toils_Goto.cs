using HarmonyLib;
using System.Linq;
using Verse;
using Verse.AI;

namespace AnomalyPatch.DontBlockDoors
{
    [HarmonyPatch(typeof(Toils_Goto))]
    [HarmonyPatch(nameof(Toils_Goto.GotoThing))]
    [HarmonyPatch(new[] { typeof(TargetIndex), typeof(PathEndMode), typeof(bool) })]
    public static class Patch_Toils_Goto
    {
        public static void Postfix(TargetIndex ind, PathEndMode peMode, bool canGotoSpawnedParent, ref Toil __result)
        {
            if (peMode == PathEndMode.Touch)
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
                    IntVec3 bestCell;
                    if ((thing == null || !(thing is Pawn)) && !actor.InMentalState && GenAdj.AdjacentCells.Select(c => dest.Cell + c).Where(c => actor.CanReach(c, PathEndMode.OnCell, Danger.Deadly) && !c.GetRegion(actor.Map).IsDoorway).TryMinBy(c => actor.Position.DistanceTo(c), out bestCell))
                    {
                        actor.pather.StartPath(bestCell, PathEndMode.OnCell);
                    }
                    else
                    {
                        actor.pather.StartPath(dest, peMode);
                    }
                };
            }
        }
    }
}
