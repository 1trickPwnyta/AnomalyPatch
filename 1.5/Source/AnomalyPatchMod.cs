using HarmonyLib;
using Verse;

namespace AnomalyPatch
{
    public class AnomalyPatchMod : Mod
    {
        public const string PACKAGE_ID = "anomalypatch.1trickPwnyta";
        public const string PACKAGE_NAME = "1trickPwnyta's Anomaly Patch";

        public AnomalyPatchMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
