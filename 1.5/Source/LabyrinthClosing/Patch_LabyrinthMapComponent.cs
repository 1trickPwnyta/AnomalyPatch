using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace AnomalyPatch.LabyrinthClosing
{
    [HarmonyPatch(typeof(LabyrinthMapComponent))]
    [HarmonyPatch("TeleportPawnsClosing")]
    public static class Patch_LabyrinthMapComponent_TeleportPawnsClosing
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundThings = false;
            bool foundSkip = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundThings && instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == AnomalyPatchRefs.f_Map_spawnedThings)
                {
                    foundThings = true;
                }
                if (foundThings && !foundSkip && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == AnomalyPatchRefs.m_SkipUtility_SkipTo)
                {
                    foundSkip = true;
                }
                if (foundSkip && !finished && instruction.opcode == OpCodes.Pop)
                {
                    yield return new CodeInstruction(OpCodes.Call, AnomalyPatchRefs.m_LabyrinthUtility_ForbidIfOutsideHomeZone);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
