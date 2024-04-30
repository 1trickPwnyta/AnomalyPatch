using RimWorld;

namespace AnomalyPatch.StudyAndSuppressByDefault
{
    // Patched manually in mod constructor
    public static class Patch_CompActivity_ctor
    {
        public static void Postfix(CompActivity __instance)
        {
            __instance.suppressionEnabled = true;
        }
    }
}
