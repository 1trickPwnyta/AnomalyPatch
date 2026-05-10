using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;

namespace AnomalyPatch
{
    public class SpecialModSettings_Multipatch_Anomaly : SpecialModSettings_Multipatch<Settings>
    {
        protected override string SettingKeyPrefix => "AnomalyPatch";
    }

    public static class Category
    {
        public const string Incidents = "AnomalyPatch_Incidents";
        public const string Study = "AnomalyPatch_Study";
        public const string Rituals = "AnomalyPatch_Rituals";
        public const string Entities = "AnomalyPatch_Entities";
        public const string Misc = "AnomalyPatch_Misc";
    }

    public enum Settings
    {
        [MultipatchSetting(Category.Incidents)] CharacterHighlighting,
        [MultipatchSetting(Category.Incidents)] DeathPallResurrectionSound,
        [MultipatchSetting(Category.Incidents)] HorrorMusic,
        [MultipatchSetting(Category.Incidents)] LabyrinthClosing,
        [MultipatchSetting(Category.Incidents, bugFix: true)] QuestIncidentMapFix,

        [MultipatchSetting(Category.Study)] StudyAndSuppressByDefault,
        [MultipatchSetting(Category.Study)] NoProjectNoStudy,
        [MultipatchSetting(Category.Study)] StopSuppression,
        [MultipatchSetting(Category.Study)] DangerousActivityLevels,
        [MultipatchSetting(Category.Study)] BioferriteHarvesterMultipleSelection,
        [MultipatchSetting(Category.Study)] ExtractBioferriteByDefault,
        [MultipatchSetting(Category.Study)] DontBlockDoors,
        [MultipatchSetting(Category.Study, enablerType: typeof(SettingEnabler_DontBlockDoors), indentLevel: 1)] DontBlockPrisonDoors,
        [MultipatchSetting(Category.Study)] HoldingPlatformAlert,

        [MultipatchSetting(Category.Rituals)] RitualDialogSorting,
        [MultipatchSetting(Category.Rituals)] PsychicRitualZoning,
        [MultipatchSetting(Category.Rituals)] RitualTargetsDontNeedRescue,
        [MultipatchSetting(Category.Rituals, bugFix: true)] DevourerWaterAssaultFix,

        [MultipatchSetting(Category.Entities)] ForbidMonolithCorpses,
        [MultipatchSetting(Category.Entities)] DisableDisturbingVision,
        [MultipatchSetting(Category.Entities)] DontHideStats,
        [MultipatchSetting(Category.Entities)] GhoulHunting,
        [MultipatchSetting(Category.Entities)] FoodPriority,
        [MultipatchSetting(Category.Entities)] DeadGhoulsInColonistBar,
        [MultipatchSetting(Category.Entities)] CreepJoinerLove,
        [MultipatchSetting(Category.Entities, restartRequired: true, bugFix: true)] CreepjoinerBodyTypeFix,

        [MultipatchSetting(Category.Misc)] AtmosphericHeaterFactor,
        [MultipatchSetting(Category.Misc, enablerType: typeof(SettingEnabler_Biotech), restartRequired: true)] InhumanPregnancyAttitude,
        [MultipatchSetting(Category.Misc)] AvoidDreadLeather
    }

    public class SettingEnabler_DontBlockDoors : ISettingEnabler
    {
        public bool Enabled() => Settings.DontBlockDoors.Enabled();
    }
}
