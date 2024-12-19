using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.CaravanDeathRefusal
{
    [HarmonyPatch(typeof(Hediff_DeathRefusal))]
    [HarmonyPatch("Use")]
    public static class Patch_Hediff_DeathRefusal_Use
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Brfalse_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, typeof(Hediff).Field("pawn"));
                    yield return new CodeInstruction(OpCodes.Call, typeof(Pawn).Method("get_MapHeld"));
                    yield return instruction;
                    continue;
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(Hediff_DeathRefusal))]
    [HarmonyPatch("TryTriggerReadyEffect")]
    public static class Patch_Hediff_DeathRefusal_TryTriggerReadyEffect
    {
        public static bool Prefix(Hediff_DeathRefusal __instance)
        {
            return __instance.pawn.MapHeld != null;
        }
    }
}
