using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace AnomalyPatch.CaravanDeathRefusal
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch(nameof(Pawn.Strip))]
    public static class Patch_Pawn
    {
        public static void Prefix(Pawn __instance)
        {
            if (AnomalyPatchSettings.CaravanDeathRefusal)
            {
                Caravan caravan = __instance.GetCaravan();
                if (caravan != null)
                {
                    if (!caravan.PawnsListForReading.Any(p => caravan.IsOwner(p)))
                    {
                        List<Pawn> inventoryPawnList = caravan.Goods.Where(t => t is Corpse).Select(c => ((Corpse)c).InnerPawn).Where(p => DeathRefusalUtility.HasPlayerControlledDeathRefusal(p) && p.inventory != null).ToList();
                        PatchUtility_Pawn.MoveAllInventoryToSomeoneElseForced(__instance, inventoryPawnList);
                        if (!DeathRefusalUtility.HasPlayerControlledDeathRefusal(__instance))
                        {
                            PatchUtility_Pawn.MoveAllApparelToSomeonesInventoryForced(__instance, inventoryPawnList);
                        }
                        PatchUtility_Pawn.MoveAllEquipmentToSomeonesInventoryForced(__instance, inventoryPawnList);
                    }
                }
            }
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundApparel = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundApparel && instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == typeof(Pawn).Field(nameof(Pawn.apparel)))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldnull);
                    yield return new CodeInstruction(OpCodes.Ceq);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, typeof(DeathRefusalUtility).Method(nameof(DeathRefusalUtility.HasPlayerControlledDeathRefusal)));
                    yield return new CodeInstruction(OpCodes.Or);
                    foundApparel = true;
                    continue;
                }
                if (foundApparel && !finished && instruction.opcode == OpCodes.Brfalse_S)
                {
                    instruction.opcode = OpCodes.Brtrue_S;
                    finished = true;
                }

                yield return instruction;
            }
        }
    }

    public static class PatchUtility_Pawn
    {
        public static void MoveAllInventoryToSomeoneElseForced(Pawn fromPawn, List<Pawn> candidates)
        {
            foreach (Thing thing in fromPawn.inventory.innerContainer)
            {
                candidates.RandomElement().inventory.innerContainer.TryTransferToContainer(thing, fromPawn.inventory.innerContainer, thing.stackCount);
            }
        }

        public static void MoveAllApparelToSomeonesInventoryForced(Pawn fromPawn, List<Pawn> candidates)
        {
            foreach (Apparel apparel in fromPawn.apparel.WornApparel)
            {
                fromPawn.apparel.Remove(apparel);
                candidates.RandomElement().inventory.innerContainer.TryAdd(apparel);
            }
        }

        public static void MoveAllEquipmentToSomeonesInventoryForced(Pawn fromPawn, List<Pawn> candidates)
        {
            foreach (ThingWithComps thing in fromPawn.equipment.AllEquipmentListForReading)
            {
                fromPawn.equipment.Remove(thing);
                candidates.RandomElement().inventory.innerContainer.TryAdd(thing);
            }
        }
    }
}
