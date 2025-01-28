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

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            listingStandard.CheckboxLabeled("AnomalyPatch_AtmosphericHeaterFactor".Translate(), ref AtmosphericHeaterFactor);
            listingStandard.CheckboxLabeled("AnomalyPatch_BioferriteHarvesterMultipleSelection".Translate(), ref BioferriteHarvesterMultipleSelection);
            listingStandard.CheckboxLabeled("AnomalyPatch_CharacterHighlighting".Translate(), ref CharacterHighlighting);
            listingStandard.CheckboxLabeled("AnomalyPatch_DangerousActivityLevels".Translate(), ref DangerousActivityLevels);
            listingStandard.CheckboxLabeled("AnomalyPatch_DeathPallResurrectionSound".Translate(), ref DeathPallResurrectionSound);
            listingStandard.CheckboxLabeled("AnomalyPatch_DevourerWaterAssaultFix".Translate(), ref DevourerWaterAssaultFix);
            listingStandard.CheckboxLabeled("AnomalyPatch_DontBlockDoors".Translate(), ref DontBlockDoors);
            listingStandard.CheckboxLabeled("AnomalyPatch_FoodPriority".Translate(), ref FoodPriority);
            listingStandard.CheckboxLabeled("AnomalyPatch_GhoulHunting".Translate(), ref GhoulHunting);
            listingStandard.CheckboxLabeled("AnomalyPatch_HorrorMusic".Translate(), ref HorrorMusic);
            listingStandard.CheckboxLabeled("AnomalyPatch_LabyrinthClosing".Translate(), ref LabyrinthClosing);
            listingStandard.CheckboxLabeled("AnomalyPatch_NoProjectNoStudy".Translate(), ref NoProjectNoStudy);
            listingStandard.CheckboxLabeled("AnomalyPatch_PsychicRitualZoning".Translate(), ref PsychicRitualZoning);
            listingStandard.CheckboxLabeled("AnomalyPatch_RitualDialogSorting".Translate(), ref RitualDialogSorting);
            listingStandard.CheckboxLabeled("AnomalyPatch_StudyAndSuppressByDefault".Translate(), ref StudyAndSuppressByDefault);
            listingStandard.CheckboxLabeled("AnomalyPatch_DontHideStats".Translate(), ref DontHideStats);
            listingStandard.CheckboxLabeled("AnomalyPatch_ForbidMonolithCorpses".Translate(), ref ForbidMonolithCorpses);
            listingStandard.CheckboxLabeled("AnomalyPatch_StopSuppression".Translate(), ref StopSuppression);
            listingStandard.CheckboxLabeled("AnomalyPatch_HoldingPlatformAlert".Translate(), ref HoldingPlatformAlert);
            listingStandard.CheckboxLabeled("AnomalyPatch_CreepJoinerLove".Translate(), ref CreepJoinerLove);
            listingStandard.CheckboxLabeled("AnomalyPatch_UnnaturalDarknessMapFix".Translate(), ref UnnaturalDarknessMapFix);
            listingStandard.CheckboxLabeled("AnomalyPatch_DeadGhoulsInColonistBar".Translate(), ref DeadGhoulsInColonistBar);
            listingStandard.CheckboxLabeled("AnomalyPatch_DisableDisturbingVision".Translate(), ref DisableDisturbingVision);
            listingStandard.CheckboxLabeled("AnomalyPatch_InhumanPregnancyAttitude".Translate() + " " + "AnomalyPatch_RestartRequired".Translate(), ref InhumanPregnancyAttitude);

            listingStandard.End();
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
        }
    }
}
