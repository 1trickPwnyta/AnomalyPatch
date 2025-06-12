using AnomalyPatch.CreepJoinerLove;
using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace AnomalyPatch
{
    public class AnomalyPatchMod : Mod
    {
        public const string PACKAGE_ID = "anomalypatch.1trickPwnyta";
        public const string PACKAGE_NAME = "1trickPwnyta's Anomaly Patch";

        public static AnomalyPatchSettings Settings;

        public static bool BigAndSmall = AccessTools.TypeByName("BigAndSmall.RomancePatches") != null;

        public AnomalyPatchMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<AnomalyPatchSettings>();

            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();
            harmony.Patch(typeof(CompActivity).GetConstructor(new Type[] { }), null, typeof(StudyAndSuppressByDefault.Patch_CompActivity_ctor).GetMethod("Postfix"));
            harmony.Patch(typeof(JobDriver_ActivitySuppression).GetNestedType("<>c__DisplayClass9_0", BindingFlags.NonPublic).Method("<TrySuppress>b__1"), null, null, typeof(StopSuppression.Patch_JobDriver_ActivitySuppression).Method(nameof(StopSuppression.Patch_JobDriver_ActivitySuppression.Transpiler)));
            harmony.Patch(typeof(PawnPsychicRitualRoleSelectionWidget).GetConstructor(new[] { typeof(PsychicRitualDef), typeof(PsychicRitualCandidatePool), typeof(PsychicRitualRoleAssignments) }), null, typeof(RitualDialogSorting.Patch_PawnPsychicRitualRoleSelectionWidget).GetMethod("Postfix"));
            if (!BigAndSmall && AnomalyPatchSettings.CreepJoinerLove)
            {
                harmony.Patch(typeof(Pawn_RelationsTracker).Method(nameof(Pawn_RelationsTracker.SecondaryLovinChanceFactor)), null, null, typeof(Patch_Pawn_RelationsTracker).Method(nameof(Patch_Pawn_RelationsTracker.Transpiler)));
            }

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
