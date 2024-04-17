using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnomalyPatch.HorrorMusic
{
    public static class MapUtility
    {
        public static List<Map> GetPlayerOccupiedMaps()
        {
            return Find.Maps.Where(map => map.mapPawns.AnyColonistSpawned || map.IsPlayerHome).ToList();
        }
    }
}
