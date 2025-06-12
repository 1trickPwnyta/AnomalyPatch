namespace AnomalyPatch
{
    public static class Debug
    {
        public static void Log(object message)
        {
#if DEBUG
            Verse.Log.Message($"[{AnomalyPatchMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
