using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace AnomalyPatch.CaravanDeathRefusal
{
    public class WorldObjectComp_DeathRefusal : WorldObjectComp
    {
        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (AnomalyPatchSettings.CaravanDeathRefusal)
            {
                Caravan caravan = (Caravan)parent;
                foreach (Corpse corpse in caravan.Goods.Where(t => t is Corpse))
                {
                    if (DeathRefusalUtility.HasPlayerControlledDeathRefusal(corpse.InnerPawn))
                    {
                        Hediff_DeathRefusal hediff = corpse.InnerPawn.health.hediffSet.GetFirstHediff<Hediff_DeathRefusal>();
                        foreach (Gizmo gizmo in hediff.GetGizmos())
                        {
                            Command_ActionWithLimitedUseCount command = gizmo as Command_ActionWithLimitedUseCount;
                            if (command != null)
                            {
                                command.defaultLabel += $" ({corpse.InnerPawn.LabelShortCap})";
                            }
                            yield return gizmo;
                        }
                    }
                }
            }
            yield break;
        }
    }
}
