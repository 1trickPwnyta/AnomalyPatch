using RimWorld;

namespace AnomalyPatch.CaravanDeathRefusal
{
    public class WorldObjectCompProperties_DeathRefusal : WorldObjectCompProperties
    {
        public WorldObjectCompProperties_DeathRefusal()
        {
            compClass = typeof(WorldObjectComp_DeathRefusal);
        }
    }
}
