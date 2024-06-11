using HarmonyLib;
using Verse;

namespace AnomalyPatch.StudyAndSuppressByDefault
{
    [HarmonyPatch(typeof(UnnaturalCorpseTracker))]
    [HarmonyPatch("SpawnNewCorpse")]
    public static class Patch_UnnaturalCorpseTracker_SpawnNewCorpse
    {
        public static void Postfix(UnnaturalCorpse ___corpse)
        {
            if (AnomalyPatchSettings.StudyAndSuppressByDefault)
            {
                ___corpse.Forbiddable.Forbidden = false;
            }
        }
    }
}
