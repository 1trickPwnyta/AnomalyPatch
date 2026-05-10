using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnomalyPatch.HorrorMusic
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.HorrorMusic)]
    [HarmonyPatch(typeof(MusicManagerPlay))]
    [HarmonyPatch(nameof(MusicManagerPlay.DangerMusicMode))]
    [HarmonyPatch(MethodType.Getter)]
    public static class Patch_MusicManagerPlay_get_DangerMusicMode
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(typeof(Find).PropertyGetter(nameof(Find.Maps))))
                {
                    instruction.operand = typeof(Patch_MusicManagerPlay_get_DangerMusicMode).Method(nameof(GetCombatMusicMapCandidates));
                }

                yield return instruction;
            }
        }

        private static List<Map> GetCombatMusicMapCandidates()
        {
            return Find.Maps.Where(map => !Settings.HorrorMusic.Enabled() || map.mapPawns.AnyColonistSpawned || map.IsPlayerHome).ToList();
        }
    }
}
