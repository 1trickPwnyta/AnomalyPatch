using AnomalyPatch.HorrorMusic;
using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

namespace AnomalyPatch
{
    public static class AnomalyPatchRefs
    {
        public static readonly MethodInfo m_Find_get_Maps = AccessTools.Method(typeof(Find), "get_Maps");
        public static readonly MethodInfo m_MapUtility_GetPlayerOccupiedMaps = AccessTools.Method(typeof(MapUtility), nameof(MapUtility.GetPlayerOccupiedMaps));
        public static readonly MethodInfo m_IInspectPane_get_ShouldShowPaneContents = AccessTools.Method(typeof(IInspectPane), "get_ShouldShowPaneContents");
        public static readonly MethodInfo m_InspectPaneUtility_DoPaneContentsForMultipleBioferriteHarvesters = AccessTools.Method(typeof(BioferriteHarvesterMultipleSelection.InspectPaneUtility), nameof(BioferriteHarvesterMultipleSelection.InspectPaneUtility.DoPaneContentsForMultipleBioferriteHarvesters));
        public static readonly MethodInfo m_Building_BioferriteHarvester_get_BioferritePerDay = AccessTools.Method(typeof(Building_BioferriteHarvester), "get_BioferritePerDay");
    }
}
