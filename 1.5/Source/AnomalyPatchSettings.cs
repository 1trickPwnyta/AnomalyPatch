using UnityEngine;
using Verse;

namespace AnomalyPatch
{
    public class AnomalyPatchSettings : ModSettings
    {
        public static bool AtmosphericHeaterFactor = true;
        public static bool BioferriteHarvesterMultipleSelection = true;
        public static bool CharacterHighlighting = true;
        public static bool DangerousActivityLevels = true;
        public static bool DeathPallResurrectionSound = true;
        public static bool DevourerWaterAssaultFix = true;
        public static bool DontBlockDoors = true;
        public static bool FoodPriority = true;
        public static bool GhoulHunting = true;
        public static bool HorrorMusic = true;
        public static bool LabyrinthClosing = true;
        public static bool NoProjectNoStudy = true;
        public static bool PsychicRitualZoning = true;
        public static bool RitualDialogSorting = true;
        public static bool StudyAndSuppressByDefault = true;
        public static bool DontHideStats = true;
        public static bool ForbidMonolithCorpses = true;
        public static bool StopSuppression = true;
        public static bool HoldingPlatformAlert = true;
        public static bool CreepJoinerLove = true;
        public static bool UnnaturalDarknessMapFix = true;
        public static bool DeadGhoulsInColonistBar = true;
        public static bool DisableDisturbingVision = true;
        public static bool InhumanPregnancyAttitude = true;
        public static bool RitualTargetsDontNeedRescue = true;

        private static Vector2 scrollPosition;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            float height = 25f;
            Rect viewRect = new Rect(0f, 0f, inRect.width - 20f, (height + 2f) * 25);
            Widgets.BeginScrollView(inRect, ref scrollPosition, viewRect);

            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(viewRect);

            listingStandard.CheckboxLabeled("AnomalyPatch_AtmosphericHeaterFactor".Translate(), ref AtmosphericHeaterFactor, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_BioferriteHarvesterMultipleSelection".Translate(), ref BioferriteHarvesterMultipleSelection, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_CharacterHighlighting".Translate(), ref CharacterHighlighting, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_DangerousActivityLevels".Translate(), ref DangerousActivityLevels, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_DeathPallResurrectionSound".Translate(), ref DeathPallResurrectionSound, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_DevourerWaterAssaultFix".Translate(), ref DevourerWaterAssaultFix, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_DontBlockDoors".Translate(), ref DontBlockDoors, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_FoodPriority".Translate(), ref FoodPriority, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_GhoulHunting".Translate(), ref GhoulHunting, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_HorrorMusic".Translate(), ref HorrorMusic, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_LabyrinthClosing".Translate(), ref LabyrinthClosing, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_NoProjectNoStudy".Translate(), ref NoProjectNoStudy, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_PsychicRitualZoning".Translate(), ref PsychicRitualZoning, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_RitualDialogSorting".Translate(), ref RitualDialogSorting, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_StudyAndSuppressByDefault".Translate(), ref StudyAndSuppressByDefault, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_DontHideStats".Translate(), ref DontHideStats, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_ForbidMonolithCorpses".Translate(), ref ForbidMonolithCorpses, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_StopSuppression".Translate(), ref StopSuppression, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_HoldingPlatformAlert".Translate(), ref HoldingPlatformAlert, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_CreepJoinerLove".Translate(), ref CreepJoinerLove, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_UnnaturalDarknessMapFix".Translate(), ref UnnaturalDarknessMapFix, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_DeadGhoulsInColonistBar".Translate(), ref DeadGhoulsInColonistBar, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_DisableDisturbingVision".Translate(), ref DisableDisturbingVision, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_InhumanPregnancyAttitude".Translate() + " " + "AnomalyPatch_RestartRequired".Translate(), ref InhumanPregnancyAttitude, null, height);
            listingStandard.CheckboxLabeled("AnomalyPatch_RitualTargetsDontNeedRescue".Translate(), ref RitualTargetsDontNeedRescue, null, height);

            listingStandard.End();

            Widgets.EndScrollView();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref AtmosphericHeaterFactor, "AtmosphericHeaterFactor", true);
            Scribe_Values.Look(ref BioferriteHarvesterMultipleSelection, "BioferriteHarvesterMultipleSelection", true);
            Scribe_Values.Look(ref CharacterHighlighting, "CharacterHighlighting", true);
            Scribe_Values.Look(ref DangerousActivityLevels, "DangerousActivityLevels", true);
            Scribe_Values.Look(ref DeathPallResurrectionSound, "DeathPallResurrectionSound", true);
            Scribe_Values.Look(ref DevourerWaterAssaultFix, "DevourerWaterAssaultFix", true);
            Scribe_Values.Look(ref DontBlockDoors, "DontBlockDoors", true);
            Scribe_Values.Look(ref FoodPriority, "FoodPriority", true);
            Scribe_Values.Look(ref GhoulHunting, "GhoulHunting", true);
            Scribe_Values.Look(ref HorrorMusic, "HorrorMusic", true);
            Scribe_Values.Look(ref LabyrinthClosing, "LabyrinthClosing", true);
            Scribe_Values.Look(ref NoProjectNoStudy, "NoProjectNoStudy", true);
            Scribe_Values.Look(ref PsychicRitualZoning, "PsychicRitualZoning", true);
            Scribe_Values.Look(ref RitualDialogSorting, "RitualDialogSorting", true);
            Scribe_Values.Look(ref StudyAndSuppressByDefault, "StudyAndSuppressByDefault", true);
            Scribe_Values.Look(ref DontHideStats, "DontHideStats", true);
            Scribe_Values.Look(ref ForbidMonolithCorpses, "ForbidMonolithCorpses", true);
            Scribe_Values.Look(ref StopSuppression, "StopSuppression", true);
            Scribe_Values.Look(ref HoldingPlatformAlert, "HoldingPlatformAlert", true);
            Scribe_Values.Look(ref CreepJoinerLove, "CreepJoinerLove", true);
            Scribe_Values.Look(ref UnnaturalDarknessMapFix, "UnnaturalDarknessMapFix", true);
            Scribe_Values.Look(ref DeadGhoulsInColonistBar, "DeadGhoulsInColonistBar", true);
            Scribe_Values.Look(ref DisableDisturbingVision, "DisableDisturbingVision", true);
            Scribe_Values.Look(ref InhumanPregnancyAttitude, "InhumanPregnancyAttitude", true);
            Scribe_Values.Look(ref RitualTargetsDontNeedRescue, "RitualTargetsDontNeedRescue", true);
        }
    }
}
