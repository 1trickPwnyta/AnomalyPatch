using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace AnomalyPatch.DontBlockDoors
{
    public static class DontBlockDoorsUtility
    {
        public static bool IsContainmentOrPrisonDoorway(this Region region)
        {
            if (region != null && region.IsDoorway)
            {
                foreach (IntVec3 cell in region.Cells)
                {
                    foreach (IntVec3 cardinalCell in GenAdj.CardinalDirections.Select(c => cell + c))
                    {
                        Room room = cardinalCell.GetRoom(region.Map);
                        if (room != null && room.ProperRoom)
                        {
                            if (room.IsPrisonCell || room.ContainedAndAdjacentThings.Any(t => ThingRequestGroup.EntityHolder.Includes(t.def)))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static void GotoBestCell(LocalTargetInfo target, Pawn actor, PathEndMode peMode)
        {
            IEnumerable<IntVec3> adjacentCells;
            if (target.Thing == null)
            {
                adjacentCells = GenAdj.AdjacentCells.Select(c => target.Cell + c);
            }
            else
            {
                adjacentCells = GenAdj.CellsAdjacent8Way(target.Thing);
            }
            IntVec3 bestCell;
            if (actor.IsPlayerControlled && adjacentCells.Any(c => c.GetRegion(actor.Map).IsContainmentOrPrisonDoorway()) && adjacentCells.Where(c => actor.CanReach(c, PathEndMode.OnCell, Danger.Deadly) && !c.GetRegion(actor.Map).IsContainmentOrPrisonDoorway() && !c.HasTrap(actor.Map)).TryMinBy(c => actor.Position.DistanceTo(c), out bestCell))
            {
                actor.pather.StartPath(bestCell, PathEndMode.OnCell);
            }
            else
            {
                actor.pather.StartPath(target, peMode);
            }
        }

        public static void GotoBestCell(IntVec3 targetCell, Pawn actor, PathEndMode peMode)
        {
            GotoBestCell(new LocalTargetInfo(targetCell), actor, peMode);
        }

        public static bool HasTrap(this IntVec3 cell, Map map)
        {
            Building building = cell.GetEdifice(map);
            if (building != null)
            {
                BuildingProperties props = building.def.building;
                if (props != null)
                {
                    return props.isTrap;
                }
            }
            return false;
        }
    }
}
