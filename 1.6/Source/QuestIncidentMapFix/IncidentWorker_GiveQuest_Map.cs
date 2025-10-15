using RimWorld.QuestGen;
using RimWorld;
using Verse;

namespace AnomalyPatch.QuestIncidentMapFix
{
    public class IncidentWorker_GiveQuest_Map : IncidentWorker_GiveQuest
    {
        protected override void GiveQuest(IncidentParms parms, QuestScriptDef questDef)
        {
            Slate slate = new Slate();
            slate.Set("points", parms.points);
            if (AnomalyPatchSettings.QuestIncidentMapFix)
            {
                slate.Set("map", (Map)parms.target);
            }
            Quest quest = QuestUtility.GenerateQuestAndMakeAvailable(questDef, slate);
            if (!quest.hidden && quest.root.sendAvailableLetter)
            {
                QuestUtility.SendLetterQuestAvailable(quest);
            }
        }
    }
}
