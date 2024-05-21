using AnomalyPatch.AtmosphericHeaterFactor;
using AnomalyPatch.HorrorMusic;
using AnomalyPatch.LabyrinthClosing;
using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using Verse.AI.Group;

namespace AnomalyPatch
{
    public static class AnomalyPatchRefs
    {
        public static readonly FieldInfo f_Map_spawnedThings = AccessTools.Field(typeof(Map), nameof(Map.spawnedThings));
        public static readonly FieldInfo f_Pawn_PlayerSettings_pawn = AccessTools.Field(typeof(Pawn_PlayerSettings), "pawn");

        public static readonly MethodInfo m_ModsConfig_get_AnomalyActive = AccessTools.Method(typeof(ModsConfig), "get_AnomalyActive");
        public static readonly MethodInfo m_Find_get_Maps = AccessTools.Method(typeof(Find), "get_Maps");
        public static readonly MethodInfo m_MapUtility_GetPlayerOccupiedMaps = AccessTools.Method(typeof(MapUtility), nameof(MapUtility.GetPlayerOccupiedMaps));
        public static readonly MethodInfo m_IInspectPane_get_ShouldShowPaneContents = AccessTools.Method(typeof(IInspectPane), "get_ShouldShowPaneContents");
        public static readonly MethodInfo m_InspectPaneUtility_DoPaneContentsForMultipleBioferriteHarvesters = AccessTools.Method(typeof(BioferriteHarvesterMultipleSelection.InspectPaneUtility), nameof(BioferriteHarvesterMultipleSelection.InspectPaneUtility.DoPaneContentsForMultipleBioferriteHarvesters));
        public static readonly MethodInfo m_Building_BioferriteHarvester_get_BioferritePerDay = AccessTools.Method(typeof(Building_BioferriteHarvester), "get_BioferritePerDay");
        public static readonly MethodInfo m_LabyrinthUtility_ForbidIfOutsideHomeZone = AccessTools.Method(typeof(LabyrinthUtility), nameof(LabyrinthUtility.ForbidIfOutsideHomeZone));
        public static readonly MethodInfo m_SkipUtility_SkipTo = AccessTools.Method(typeof(SkipUtility), nameof(SkipUtility.SkipTo));
        public static readonly MethodInfo m_AtmosphericHeaterUtility_MapHasAtmosphericHeater = AccessTools.Method(typeof(AtmosphericHeaterUtility), nameof(AtmosphericHeaterUtility.MapHasAtmosphericHeater));
        public static readonly MethodInfo m_LordUtility_GetLord = AccessTools.Method(typeof(LordUtility), nameof(LordUtility.GetLord), new[] { typeof(Pawn) });
        public static readonly MethodInfo m_LordUtility_IsPsychicRitual = AccessTools.Method(typeof(PsychicRitualZoning.LordUtility), nameof(PsychicRitualZoning.LordUtility.IsPsychicRitual));
    }
}
