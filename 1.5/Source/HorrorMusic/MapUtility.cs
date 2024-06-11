using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnomalyPatch.HorrorMusic
{
    public static class MapUtility
    {
        public static List<Map> GetCombatMusicMapCandidates()
        {
            return Find.Maps.Where(map => !AnomalyPatchSettings.HorrorMusic || map.mapPawns.AnyColonistSpawned || map.IsPlayerHome).ToList();
        }
    }
}
