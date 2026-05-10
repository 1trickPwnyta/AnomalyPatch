using HarmonyLib;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using Verse;

namespace AnomalyPatch.StudyAndSuppressByDefault
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.StudyAndSuppressByDefault)]
    [HarmonyPatch(typeof(UnnaturalCorpseTracker))]
    [HarmonyPatch("SpawnNewCorpse")]
    public static class Patch_UnnaturalCorpseTracker_SpawnNewCorpse
    {
        public static void Postfix(UnnaturalCorpse ___corpse)
        {
            if (Settings.StudyAndSuppressByDefault.Enabled())
            {
                ___corpse.Forbiddable.Forbidden = false;
            }
        }
    }
}
