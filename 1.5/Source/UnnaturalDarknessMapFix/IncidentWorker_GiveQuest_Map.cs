using RimWorld.QuestGen;
using RimWorld;
using Verse;

namespace AnomalyPatch.UnnaturalDarknessMapFix
{
    public class IncidentWorker_GiveQuest_Map : IncidentWorker_GiveQuest
    {
        protected override void GiveQuest(IncidentParms parms, QuestScriptDef questDef)
        {
            Slate slate = new Slate();
            slate.Set<float>("points", parms.points, false);
            if (AnomalyPatchSettings.UnnaturalDarknessMapFix)
            {
                slate.Set<Map>("map", (Map)parms.target, false);
            }
            Quest quest = QuestUtility.GenerateQuestAndMakeAvailable(questDef, slate);
            if (!quest.hidden && quest.root.sendAvailableLetter)
            {
                QuestUtility.SendLetterQuestAvailable(quest);
            }
        }
    }
}
