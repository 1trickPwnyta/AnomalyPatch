using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System.Reflection;
using Verse;

namespace AnomalyPatch
{
    public class SpecialMod_Multipatch_Anomaly : SpecialMod_Multipatch<SpecialModSettings_Multipatch_Anomaly, Settings>
    {
        public const string PACKAGE_ID = "1trickpwnyta.anomalypatch";
        public const string PACKAGE_NAME = "1trickPwnyta's Anomaly Patch";

        public SpecialMod_Multipatch_Anomaly(ModContentPack content) : base(content)
        {
        }

        protected override string PackageName => PACKAGE_NAME;

        protected override string PackageId => PACKAGE_ID;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var harmony = new Harmony(PackageId);
            harmony.PatchCategory(HarmonyPatch_Compatibility.EnabledCategory);
            SpecialModSettings_Multipatch_Anomaly settings = Settings as SpecialModSettings_Multipatch_Anomaly;
            if (settings.ShouldEnableCodeForSetting(AnomalyPatch.Settings.StopSuppression))
            {
                harmony.Patch(typeof(JobDriver_ActivitySuppression).GetNestedType("<>c__DisplayClass9_0", BindingFlags.NonPublic).Method("<TrySuppress>b__1"), transpiler: typeof(StopSuppression.Patch_JobDriver_ActivitySuppression).Method(nameof(StopSuppression.Patch_JobDriver_ActivitySuppression.Transpiler)));
            }
            Log.Info("Ready.");
        }
    }
}
