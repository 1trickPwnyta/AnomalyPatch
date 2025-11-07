using RimWorld;
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
        public static bool QuestIncidentMapFix = true;
        public static bool DeadGhoulsInColonistBar = true;
        public static bool DisableDisturbingVision = true;
        public static bool InhumanPregnancyAttitude = true;
        public static bool RitualTargetsDontNeedRescue = true;
        public static bool AvoidDreadLeather = true;
        public static bool CreepjoinerBodyTypeFix = true;
        public static bool SightstealerArrivalMapFix = true;
        public static bool DontBlockPrisonDoors = true;

        private static Vector2 scrollPosition;
        private static float y;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Rect viewRect = new Rect(0f, 0f, inRect.width - 20f, y);
            Widgets.BeginScrollView(inRect, ref scrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard() { maxOneColumn = true };
            listing.Begin(viewRect);

            DoHeader(listing, "AnomalyPatch_Incidents");
            DoSetting(listing, "AnomalyPatch_CharacterHighlighting", ref CharacterHighlighting);
            DoSetting(listing, "AnomalyPatch_DeathPallResurrectionSound", ref DeathPallResurrectionSound);
            DoSetting(listing, "AnomalyPatch_HorrorMusic", ref HorrorMusic);
            DoSetting(listing, "AnomalyPatch_LabyrinthClosing", ref LabyrinthClosing);
            DoSetting(listing, "AnomalyPatch_UnnaturalDarknessMapFix", ref QuestIncidentMapFix, bugFix: true);

            listing.Gap();

            DoHeader(listing, "AnomalyPatch_Study");
            DoSetting(listing, "AnomalyPatch_StudyAndSuppressByDefault", ref StudyAndSuppressByDefault);
            DoSetting(listing, "AnomalyPatch_NoProjectNoStudy", ref NoProjectNoStudy);
            DoSetting(listing, "AnomalyPatch_StopSuppression", ref StopSuppression);
            DoSetting(listing, "AnomalyPatch_DangerousActivityLevels", ref DangerousActivityLevels);
            DoSetting(listing, "AnomalyPatch_BioferriteHarvesterMultipleSelection", ref BioferriteHarvesterMultipleSelection);
            DoSetting(listing, "AnomalyPatch_DontBlockDoors", ref DontBlockDoors);
            DoSetting(listing, "AnomalyPatch_DontBlockPrisonDoors", ref DontBlockPrisonDoors, dependsOn: DontBlockDoors, indentLevel: 1);
            DoSetting(listing, "AnomalyPatch_HoldingPlatformAlert", ref HoldingPlatformAlert);

            listing.Gap();

            DoHeader(listing, "AnomalyPatch_Rituals");
            DoSetting(listing, "AnomalyPatch_RitualDialogSorting", ref RitualDialogSorting);
            DoSetting(listing, "AnomalyPatch_PsychicRitualZoning", ref PsychicRitualZoning);
            DoSetting(listing, "AnomalyPatch_RitualTargetsDontNeedRescue", ref RitualTargetsDontNeedRescue);
            DoSetting(listing, "AnomalyPatch_DevourerWaterAssaultFix", ref DevourerWaterAssaultFix, bugFix: true);

            listing.Gap();

            DoHeader(listing, "AnomalyPatch_Entities");
            DoSetting(listing, "AnomalyPatch_ForbidMonolithCorpses", ref ForbidMonolithCorpses);
            DoSetting(listing, "AnomalyPatch_DisableDisturbingVision", ref DisableDisturbingVision);
            DoSetting(listing, "AnomalyPatch_DontHideStats", ref DontHideStats);
            DoSetting(listing, "AnomalyPatch_GhoulHunting", ref GhoulHunting);
            DoSetting(listing, "AnomalyPatch_FoodPriority", ref FoodPriority);
            DoSetting(listing, "AnomalyPatch_DeadGhoulsInColonistBar", ref DeadGhoulsInColonistBar);
            DoSetting(listing, "AnomalyPatch_CreepJoinerLove", ref CreepJoinerLove, restartRequired: true, dependsOn: !AnomalyPatchMod.BigAndSmall);
            DoSetting(listing, "AnomalyPatch_CreepjoinerBodyTypeFix", ref CreepjoinerBodyTypeFix, restartRequired: true, bugFix: true);

            listing.Gap();

            DoHeader(listing, "AnomalyPatch_Misc");
            DoSetting(listing, "AnomalyPatch_AtmosphericHeaterFactor", ref AtmosphericHeaterFactor);
            DoSetting(listing, "AnomalyPatch_InhumanPregnancyAttitude", ref InhumanPregnancyAttitude, restartRequired: true);
            DoSetting(listing, "AnomalyPatch_AvoidDreadLeather", ref AvoidDreadLeather);

            y = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
        }

        private static void DoHeader(Listing_Standard listing, string key)
        {
            using (new TextBlock(GameFont.Medium))
            {
                listing.Label(key.Translate());
            }   
            listing.GapLine();
        }

        private static void DoSetting(Listing_Standard listing, string key, ref bool setting, bool restartRequired = false, bool bugFix = false, bool dependsOn = true, int indentLevel = 0)
        {
            if (dependsOn)
            {
                string indent = new string(' ', indentLevel * 2);
                listing.CheckboxLabeled(indent + (bugFix ? "AnomalyPatch_BugFix".Translate() + ": " : TaggedString.Empty) + key.Translate() + (restartRequired ? " " + "AnomalyPatch_RestartRequired".Translate() : TaggedString.Empty), ref setting);
            }
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
            Scribe_Values.Look(ref QuestIncidentMapFix, "UnnaturalDarknessMapFix", true);
            Scribe_Values.Look(ref DeadGhoulsInColonistBar, "DeadGhoulsInColonistBar", true);
            Scribe_Values.Look(ref DisableDisturbingVision, "DisableDisturbingVision", true);
            Scribe_Values.Look(ref InhumanPregnancyAttitude, "InhumanPregnancyAttitude", true);
            Scribe_Values.Look(ref RitualTargetsDontNeedRescue, "RitualTargetsDontNeedRescue", true);
            Scribe_Values.Look(ref AvoidDreadLeather, "AvoidDreadLeather", true);
            Scribe_Values.Look(ref CreepjoinerBodyTypeFix, "CreepjoinerBodyTypeFix", true);
            Scribe_Values.Look(ref DontBlockPrisonDoors, "DontBlockPrisonDoors", true);
        }
    }
}
