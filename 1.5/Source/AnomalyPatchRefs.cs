using AnomalyPatch.AtmosphericHeaterFactor;
using AnomalyPatch.ForbidMonolithCorpses;
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
        public static readonly FieldInfo f_AnomalyPatchSettings_CharacterHighlighting = AccessTools.Field(typeof(AnomalyPatchSettings), nameof(AnomalyPatchSettings.CharacterHighlighting));
        public static readonly FieldInfo f_AnomalyPatchSettings_DeathPallResurrectionSound = AccessTools.Field(typeof(AnomalyPatchSettings), nameof(AnomalyPatchSettings.DeathPallResurrectionSound));
        public static readonly FieldInfo f_Map_spawnedThings = AccessTools.Field(typeof(Map), nameof(Map.spawnedThings));
        public static readonly FieldInfo f_Pawn_PlayerSettings_pawn = AccessTools.Field(typeof(Pawn_PlayerSettings), "pawn");
        public static readonly FieldInfo f_MessageTypeDefOf_NegativeEvent = AccessTools.Field(typeof(MessageTypeDefOf), nameof(MessageTypeDefOf.NegativeEvent));
        public static readonly FieldInfo f_MessageTypeDefOf_SilentInput = AccessTools.Field(typeof(MessageTypeDefOf), nameof(MessageTypeDefOf.SilentInput));
        public static readonly FieldInfo f_ThingDef_hideStats = AccessTools.Field(typeof(ThingDef), nameof(ThingDef.hideStats));
        public static readonly FieldInfo f_AnomalyPatch_Settings_StopSuppression = AccessTools.Field(typeof(AnomalyPatchSettings), nameof(AnomalyPatchSettings.StopSuppression));

        public static readonly MethodInfo m_ModsConfig_get_AnomalyActive = AccessTools.Method(typeof(ModsConfig), "get_AnomalyActive");
        public static readonly MethodInfo m_Find_get_Maps = AccessTools.Method(typeof(Find), "get_Maps");
        public static readonly MethodInfo m_MapUtility_GetCombatMusicMapCandidates = AccessTools.Method(typeof(MapUtility), nameof(MapUtility.GetCombatMusicMapCandidates));
        public static readonly MethodInfo m_IInspectPane_get_ShouldShowPaneContents = AccessTools.Method(typeof(IInspectPane), "get_ShouldShowPaneContents");
        public static readonly MethodInfo m_InspectPaneUtility_DoPaneContentsForMultipleBioferriteHarvesters = AccessTools.Method(typeof(BioferriteHarvesterMultipleSelection.InspectPaneUtility), nameof(BioferriteHarvesterMultipleSelection.InspectPaneUtility.DoPaneContentsForMultipleBioferriteHarvesters));
        public static readonly MethodInfo m_Building_BioferriteHarvester_get_BioferritePerDay = AccessTools.Method(typeof(Building_BioferriteHarvester), "get_BioferritePerDay");
        public static readonly MethodInfo m_LabyrinthUtility_ForbidIfOutsideHomeZone = AccessTools.Method(typeof(LabyrinthUtility), nameof(LabyrinthUtility.ForbidIfOutsideHomeZone));
        public static readonly MethodInfo m_SkipUtility_SkipTo = AccessTools.Method(typeof(SkipUtility), nameof(SkipUtility.SkipTo));
        public static readonly MethodInfo m_AtmosphericHeaterUtility_DisableNeedWarmClothesAlert = AccessTools.Method(typeof(AtmosphericHeaterUtility), nameof(AtmosphericHeaterUtility.DisableNeedWarmClothesAlert));
        public static readonly MethodInfo m_LordUtility_GetLord = AccessTools.Method(typeof(LordUtility), nameof(LordUtility.GetLord), new[] { typeof(Pawn) });
        public static readonly MethodInfo m_LordUtility_LordRespectsAllowedArea = AccessTools.Method(typeof(PsychicRitualZoning.LordUtility), nameof(PsychicRitualZoning.LordUtility.LordRespectsAllowedArea));
        public static readonly MethodInfo m_StatUtility_ShouldHideStats = AccessTools.Method(typeof(DontHideStats.StatUtility), nameof(DontHideStats.StatUtility.ShouldHideStats));
        public static readonly MethodInfo m_GenSpawn_Spawn = AccessTools.Method(typeof(GenSpawn), nameof(GenSpawn.Spawn), new[] { typeof(Thing), typeof(IntVec3), typeof(Map), typeof(WipeMode) });
        public static readonly MethodInfo m_CorpseUtility_SetForbidden = AccessTools.Method(typeof(CorpseUtility), nameof(CorpseUtility.SetForbidden));
        public static readonly MethodInfo m_CompActivity_get_ActivityLevel = AccessTools.Method(typeof(CompActivity), "get_ActivityLevel");
        public static readonly MethodInfo m_ThingCompUtility_TryGetComp = AccessTools.Method(typeof(ThingCompUtility), nameof(ThingCompUtility.TryGetComp), new[] { typeof(Thing) }, new[] { typeof(CompActivity) });
    }
}
