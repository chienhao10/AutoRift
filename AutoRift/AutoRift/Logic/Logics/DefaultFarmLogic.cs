using System.Linq;
using AutoRift.Data;
using AutoRift.Helpers;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SharpDX.Direct3D9;
using Color = System.Drawing.Color;

namespace AutoRift.Logic
{
    public class DefaultFarmLogic : DefaultLogicContainer
    {
        private MinionWave ClosestWave { get; set; }
        private MinionWave FurthestPushedWave { get; set; }
        private float LastRandomizedTime { get; set; }
        private Vector3 LastRandomizedPosistion { get; set; }
        public static float RandomizeDelay { get; set; } = 0.350f;
        public static float RandmizeRange { get; set; } = 500;
        public override void Start()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Status.Log("Green Circle: Closest Wave");
            Status.Log("Deep Pink Circle: Most Pushed Wave");
        }

        private void Drawing_OnDraw(System.EventArgs args)
        {
            if (ClosestWave != null && FurthestPushedWave != null)
            {
                Circle.Draw(Color.Green.ToBgraColor(), 295, ClosestWave.CenterOfPolygon);
                Circle.Draw(Color.DeepPink.ToBgraColor(), 300, FurthestPushedWave.CenterOfPolygon);
            }
        }

        public override MovementData GetMovementData()
        {
            if (ClosestWave == null || FurthestPushedWave == null)
            {
                return Idol();
            }

            if (Player.Instance.Position.IsInRange(ClosestWave.CenterOfPolygon, RandmizeRange * 2))
            {
                Vector3 point;
                if (FurthestPushedWave.CenterOfPolygon.Distance(ClosestWave.CenterOfPolygon) > 2000)
                {
                    //Pushed wave is a while away, so lets walk to it.
                    //TODO: Check if safe to do so
                    point = Randomize(ClosestWave.CenterOfPolygon, RandmizeRange);
                    if (TrySafen(ref point))
                    {
                        return new MovementData(Extend(point, Player.Instance.AttackRange * 2), Orbwalker.ActiveModes.LaneClear);
                    }
                }
                point = Randomize(ClosestWave.CenterOfPolygon, RandmizeRange);
                if (TrySafen(ref point))
                {
                    // Player is within the wave range, so lets sit around it
                    return new MovementData(point,
                        Orbwalker.ActiveModes.LaneClear);
                }
            }

            if (ClosestWave != null &&
                ClosestWave.CenterOfPolygon.Distance(Nexus.Ally.Position) >
                Player.Instance.Position.Distance(Nexus.Ally.Position))
            {
                var point = Randomize(ClosestWave.CenterOfPolygon, RandmizeRange);
                if (TrySafen(ref point))
                {
                    //Closest wave is further away from allied nexus than us, so lets walk to it.
                    return new MovementData(point,
                        Orbwalker.ActiveModes.LaneClear);
                }
            }
            
            return Idol();

        }

        private bool TrySafen(ref Vector3 point)
        {
            //TODO;
            return true;
        }

        private Vector3 Extend(Vector3 vector3, float range)
        {
            return Player.Instance.Position.Distance(vector3) > range ? Player.Instance.Position.Extend(vector3, range).To3DWorld() : vector3;
        }

        private MovementData Idol()
        {
            //No waves, lets walk back towards our lane turret
            var laneTurret = Turrent.GetLaneTurret(x => x.InLane(Player.Instance.GetLane())).Position;
            if (Player.Instance.IsInRange(laneTurret, Turrent.TurrentsRange))
            {
                //Idol around turret until wave comes
                return new MovementData(Randomize(laneTurret), Orbwalker.ActiveModes.LastHit);
            }
            // Walk to Turret
            return new MovementData(Randomize(laneTurret), Orbwalker.ActiveModes.Flee);
        }

        private Vector3 Randomize(Vector3 pos, float range = -1)
        {
            if (range < 0)
            {
                range = RandmizeRange;
            }
            if (LastRandomizedTime < Game.Time - RandomizeDelay || !LastRandomizedPosistion.IsInRange(pos, range))
            {
                LastRandomizedPosistion = pos.Randomize(300);
                LastRandomizedTime = Game.Time;
            }
            return LastRandomizedPosistion;
        }

        public override void Update()
        {
            var nearMinionWaves =
                MinionManager.MinionWaves.Where(x => x.IsAlly && x.WaveLane == Player.Instance.GetLane() && !x.IsWaveDead).ToList();
            if (nearMinionWaves.Count == 0)
            {
                return;
            }

            ClosestWave = nearMinionWaves.OrderBy(x => x.CenterOfPolygon.Distance(Player.Instance)).FirstOrDefault();
            FurthestPushedWave = nearMinionWaves.OrderBy(x => x.CenterOfPolygon.Distance(Nexus.Enemy.Position)).FirstOrDefault();

        }

        public override void End()
        {
            Drawing.OnDraw -= Drawing_OnDraw;
        }
    }
}