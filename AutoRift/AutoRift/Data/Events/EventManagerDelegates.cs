using EloBuddy;

namespace AutoRift.Data.Events
{
    public static class EventManagerDelegates
    {
        public delegate void OnAlliedTurrentDamage(TurrentDamageEventArgs args);

        public delegate void OnArriveAtSpawn(OnSpawnEventArgs args);

        //Lanes
        public delegate void OnLaneTaken(LaneTakenEventArgs args);

        //Player
        public delegate void OnLevelUp(AIHeroClient player);

        //Spawn
        public delegate void OnSpawn(OnSpawnEventArgs args);

        public delegate void OnTakeNewLane(Lane.Lanes lane);

        public delegate void OnWaveSpawn(MinionWave wave);
    }
}