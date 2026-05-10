using HarmonyLib;
using RimWorld;
using SpecialSauce.ModSettings;
using SpecialSauce.Multipatch;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.AtmosphericHeaterFactor
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.AtmosphericHeaterFactor)]
    [HarmonyPatch(typeof(Alert_NeedWarmClothes))]
    [HarmonyPatch("MapWithMissingWarmClothes")]
    public static class Patch_Alert_NeedWarmClothes_MapWithMissingWarmClothes
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundCheck = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundCheck && instruction.opcode == OpCodes.Brfalse_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_2);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Patch_Alert_NeedWarmClothes_MapWithMissingWarmClothes).Method(nameof(DisableNeedWarmClothesAlert)));
                    yield return new CodeInstruction(OpCodes.Brtrue_S, instruction.operand);
                    foundCheck = true;
                    continue;
                }

                yield return instruction;
            }
        }

        private static bool DisableNeedWarmClothesAlert(Map map)
        {
            return Settings.AtmosphericHeaterFactor.Enabled() && map.listerBuildings.AllBuildingsColonistOfDef(PatchUtility_Alert_NeedWarmClothes.atmostphericHeaterDef).Any(b => (b.PowerComp as CompPowerTrader).PowerOn && b.GetComp<CompRefuelable>().HasFuel);
        }
    }

    public static class PatchUtility_Alert_NeedWarmClothes
    {
        public static ThingDef atmostphericHeaterDef = DefDatabase<ThingDef>.GetNamed("AtmosphericHeater");
    }
}
