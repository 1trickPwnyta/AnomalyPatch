using SpecialSauce.ModSettings;

namespace AnomalyPatch
{
    public static class Utility
    {
        public static bool Enabled(this Settings key) => Setting<Settings>.Get<bool>(key);
    }
}
