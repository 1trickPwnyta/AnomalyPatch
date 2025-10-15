using HarmonyLib;
using RimWorld;
using RimWorld.QuestGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.QuestIncidentMapFix
{
    [HarmonyPatch(typeof(IncidentWorker_SightstealerArrival))]
    [HarmonyPatch("TryExecuteWorker")]
    public static class Patch_IncidentWorker_SightstealerArrival
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            int index = instructionsList.FindIndex(i => i.Calls(typeof(QuestGen_Get).Method(nameof(QuestGen_Get.GetMap))));
            instructionsList.Insert(index, new CodeInstruction(OpCodes.Ldarg_1));
            instructionsList[index + 1].operand = typeof(Patch_IncidentWorker_SightstealerArrival).Method(nameof(GetMap));
            return instructionsList;
        }

        private static Map GetMap(bool mustBeInfestable, int? preferMapWithMinFreeColonists, bool canBeSpace, IncidentParms parms)
        {
            return (AnomalyPatchSettings.QuestIncidentMapFix ? parms.target as Map : null) ?? QuestGen_Get.GetMap(mustBeInfestable, preferMapWithMinFreeColonists, canBeSpace);
        }
    }
}
