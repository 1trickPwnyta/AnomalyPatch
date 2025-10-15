using HarmonyLib;
using RimWorld.QuestGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.QuestIncidentMapFix
{
    [HarmonyPatch(typeof(QuestNode_Root_SightstealerArrival))]
    [HarmonyPatch("RunInt")]
    public static class Patch_QuestNode_Root_SightstealerArrival
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.LoadsField(typeof(QuestGen).Field(nameof(QuestGen.slate))));
            instructionsList.Insert(index + 1, new CodeInstruction(OpCodes.Dup));
            CodeInstruction instruction = instructionsList.Find(i => i.Calls(typeof(QuestGen_Get).Method(nameof(QuestGen_Get.GetMap))));
            instruction.operand = typeof(Patch_QuestNode_Root_SightstealerArrival).Method(nameof(GetMap));
            return instructionsList;
        }

        private static Map GetMap(Slate slate, bool mustBeInfestable, int? preferMapWithMinFreeColonists, bool canBeSpace)
        {
            return (AnomalyPatchSettings.QuestIncidentMapFix ? slate.Get<Map>("map") : null) ?? QuestGen_Get.GetMap(mustBeInfestable, preferMapWithMinFreeColonists, canBeSpace);
        }
    }
}
