using System;
using System.Collections.Generic;
using System.Linq;
using AutoRift.Data;
using AutoRift.Helpers;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Utils;
using SharpDX;
using Color = System.Drawing.Color;

namespace AutoRift.Logic
{
    public class DefaultLaneSelectLogic : DefaultLogicContainer
    {
        public static readonly Lane.Lanes[] SupportedStartLanes = { Lane.Top, Lane.Middle, Lane.Bottom };
        public Dictionary<Lane.Lanes, Vector3> StartPosistions { get; set; }
        public static int LaneSelectDelay = 15000; //Wait 20 seconds until we select a lane.
        public Vector3[] ToLanePath { get; set; }

        public override void Init()
        {
            StartPosistions = new Dictionary<Lane.Lanes, Vector3>
            {
                [Lane.Lanes.Top] = Turrent.GetLaneTurret(x => x.GetLane() == Lane.Lanes.Top).Position,
                [Lane.Lanes.Middle] =
                    Turrent.GetLaneTurret(x => x.GetLane() == Lane.Lanes.Middle).Position,
                [Lane.Lanes.Bottom] =
                    Turrent.GetLaneTurret(x => x.GetLane() == Lane.Lanes.Bottom).Position
            };
        }

        public override void Start()
        {
            Status.Log("Stating Lane Select Logic");
            Drawing.OnDraw += Drawing_OnDraw;
            Status.Log("Buying Items", 1);
            Global.BuyItems();
            Status.Log("Leveling Spells", 1);
            Global.LevelUp();
            Status.Log($"Waiting {LaneSelectDelay / 1000f} seconds until selecting a lane.");
            Core.DelayAction(SelectStartLane, LaneSelectDelay); //Wait for delay
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (StartPosistions != null)
            {
                foreach (var startPosistion in StartPosistions)
                {
                    Drawing.DrawCircle(startPosistion.Value, 30, Color.DeepPink);
                    Drawing.DrawText(startPosistion.Value.WorldToScreen(), Color.White, startPosistion.Key.ToString(), 8);
                }
            }
            if (ToLanePath == null || ToLanePath.Length == 0)
            {
                return;
            }

            Line.DrawLine(Color.Blue, ToLanePath);
        }

        private void SelectStartLane()
        {
            Status.Log("Selecting Lane...", 1);
            //Forced Lane Select
            if (ConfigKey.ForceLane.Bool())
            {
                Global.SelectedLane = (Lane.Lanes) ConfigKey.ForceLaneValue.Int();
                ToLanePath = GetPathToLane(Global.SelectedLane);
                Status.Log($"Using Forced Lane: {Global.SelectedLane}");
                return;
            }

            //Auto Lane Select



        }

        private Vector3[] GetPathToLane(Lane.Lanes lane)
        {
            switch (lane)
            {
                case Lane.Lanes.Jungle:
                case Lane.Lanes.ChaosBase:
                case Lane.Lanes.OrderBase:
                    Status.Log($"Lane Not Supported, Selecting 'ForceLaneValue' ({(Lane.Lanes)ConfigKey.ForceLaneValue.Int()})");
                    return GetPathToLane(Global.SelectedLane = (Lane.Lanes) ConfigKey.ForceLaneValue.Int()); //Overrides selected lane, to prevent errors later on.
                default: //Top, Middle, Bottom
                    return Player.Instance.GetPath(StartPosistions[lane], true);
            }
        }

        public override void End()
        {
            Status.Log("Ended SelectLaneLogic.");
            Drawing.OnDraw -= Drawing_OnDraw;
            StartPosistions = null;
        }

        public override bool Finished()
        {
            if (ToLanePath == null || ToLanePath.Length == 0)
            {
                return false;
            }
            return Player.Instance.Position.IsInRange(ToLanePath.Last(), Player.Instance.AttackRange);
        }

        public override MovementData GetMovementData()
        {
            if (ToLanePath == null || ToLanePath.Length == 0)
            {
                return null;
            }
            var orbwalkDistance = Player.Instance.AttackRange*3;
            var resetPathDistance = Player.Instance.AttackRange*1.5f;

            if (ToLanePath.Any(x =>
                !x.IsInRange(Player.Instance.Position, Player.Instance.BoundingRadius) &&
                x.IsInRange(Player.Instance.Position, resetPathDistance)))
            {
                //Only check values that we are not standing on, if we are, its probably the first point so we are gonna ignore it.
                ToLanePath = GetPathToLane(Global.SelectedLane);
            }

            if (ToLanePath.Any(x => x.Distance(Player.Instance.Position) > orbwalkDistance))
            {
                //Path Point is 
                var first = ToLanePath.First(x => x.Distance(Player.Instance.Position) > orbwalkDistance);
                return new MovementData(Player.Instance.Position.Distance(first) > orbwalkDistance ? Player.Instance.Position.Extend(first, orbwalkDistance).To3DWorld() : first, Orbwalker.ActiveModes.LaneClear);
            }

            return new MovementData(ToLanePath.Last(), Orbwalker.ActiveModes.LaneClear);

        }

        public override void Update()
        {
            if (ToLanePath == null || ToLanePath.Length == 0)
            {
                return;
            }
            if (ToLanePath.Any(x => x.IsInRange(Player.Instance, Player.Instance.AttackRange)))
            {
                ToLanePath = GetPathToLane(Global.SelectedLane);
            }
        }
    }
}
