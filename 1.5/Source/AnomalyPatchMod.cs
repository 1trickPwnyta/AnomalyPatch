using HarmonyLib;
using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace AnomalyPatch
{
    public class AnomalyPatchMod : Mod
    {
        public const string PACKAGE_ID = "anomalypatch.1trickPwnyta";
        public const string PACKAGE_NAME = "1trickPwnyta's Anomaly Patch";

        public static AnomalyPatchSettings Settings;

        public AnomalyPatchMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();
            harmony.Patch(typeof(CompActivity).GetConstructor(new Type[] { }), null, typeof(StudyAndSuppressByDefault.Patch_CompActivity_ctor).GetMethod("Postfix"));

            Settings = GetSettings<AnomalyPatchSettings>();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            AnomalyPatchSettings.DoSettingsWindowContents(inRect);
        }
    }
}
