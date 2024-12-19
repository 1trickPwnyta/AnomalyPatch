using HarmonyLib;
using System.Collections.Generic;
using Verse;

namespace AnomalyPatch
{
    //[HarmonyPatch(typeof(MapPawns))]
    //[HarmonyPatch("get_AnyPawnBlockingMapRemoval")]
    public static class Patch_MapPawns
    {
        public static void Prefix(MapPawns __instance, Map ___map)
        {
            using (List<Thing>.Enumerator enumerator2 = ___map.listerThings.ThingsInGroup(ThingRequestGroup.Corpse).GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    Debug.Log("check " + enumerator2.Current);
                    Debug.Log("corpse: " + (enumerator2.Current as Corpse).InnerPawn);
                    Corpse corpse;
                    if ((corpse = (enumerator2.Current as Corpse)) != null && (bool)typeof(MapPawns).Method("IsValidColonyPawn").Invoke(null, new object[] { corpse.InnerPawn }))
                    {
                        Debug.Log("IT WORKS");
                        return;
                    }
                }
            }
            Debug.Log("FAIL");
        }
    }
}
