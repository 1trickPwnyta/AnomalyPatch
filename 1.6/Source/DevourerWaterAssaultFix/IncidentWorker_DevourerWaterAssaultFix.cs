using AnomalyPatch;

namespace RimWorld
{
    public class IncidentWorker_DevourerWaterAssaultFix : IncidentWorker_DevourerWaterAssault
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return base.CanFireNowSub(parms) && (!Settings.DevourerWaterAssaultFix.Enabled() || PawnsArrivalModeDefOf.EmergeFromWater.Worker.TryResolveRaidSpawnCenter(parms));
        }
    }
}
