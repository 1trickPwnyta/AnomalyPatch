using RimWorld;
using Verse;

namespace AnomalyPatch.AtmosphericHeaterFactor
{
    public static class AtmosphericHeaterUtility
    {
        public static bool DisableNeedWarmClothesAlert(Map map)
        {
            return AnomalyPatchSettings.AtmosphericHeaterFactor && map.listerBuildings.AllBuildingsColonistOfDef(DefDatabase<ThingDef>.GetNamed("AtmosphericHeater")).Any(b => (b.PowerComp as CompPowerTrader).PowerOn && b.GetComp<CompRefuelable>().HasFuel);
        }
    }
}
