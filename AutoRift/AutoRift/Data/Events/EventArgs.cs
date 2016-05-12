using System;
using EloBuddy;

namespace AutoRift.Data.Events
{
    public class OnSpawnEventArgs : EventArgs
    {
        public OnSpawnEventArgs(AIHeroClient player)
        {
            Player = player;
        }

        /// <summary>
        ///     The Player.Instance.
        /// </summary>
        public AIHeroClient Player { get; private set; }

        /// <summary>
        ///     Set this value to the lane you wish to walk to.
        /// </summary>
        public Lane.Lanes Lane
        {
            set { Global.SelectedLane = value; }
        }
    }

    public class LaneTakenEventArgs : EventArgs
    {
        public LaneTakenEventArgs(AIHeroClient player, Lane.Lanes laneTaken)
        {
            LaneTaken = laneTaken;
            Offender = player;
        }

        /// <summary>
        ///     The Player that took the lane.
        /// </summary>
        public AIHeroClient Offender { get; private set; }

        /// <summary>
        ///     What lane was taken by the AiHeroClient
        /// </summary>
        public Lane.Lanes LaneTaken { get; private set; }

        /// <summary>
        ///     Set this value to the lane you wish to take.
        /// </summary>
        public Lane.Lanes TakeLane { get; set; }
    }

    public class TurrentDamageEventArgs : EventArgs
    {
        public TurrentDamageEventArgs(Obj_AI_Turret turrent, Obj_AI_Base[] attackers, Lane.Lanes lane)
        {
            Turrent = turrent;
            Attackers = attackers;
            Lane = lane;
        }

        public Obj_AI_Turret Turrent { get; private set; }
        public Obj_AI_Base[] Attackers { get; private set; }
        public Lane.Lanes Lane { get; private set; }
    }
}