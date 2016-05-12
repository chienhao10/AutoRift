using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace AutoRift.Data
{
    public static class Lane
    {
        public enum Lanes
        {
            Middle,
            Bottom,
            Top,
            Jungle,
            ChaosBase,
            OrderBase
        }

        public const Lanes Top = Lanes.Top;
        public const Lanes Bottom = Lanes.Bottom;
        public const Lanes Middle = Lanes.Middle;
        public const Lanes Jungle = Lanes.Jungle;
        public const Lanes OrderBase = Lanes.OrderBase;
        public const Lanes ChaosBase = Lanes.ChaosBase;

        private static readonly Geometry.Polygon TopLanePos = new Geometry.Polygon
        {
            Points =
            {
                NavMesh.GetCell(15, 120).WorldPosition.To2D(),
                NavMesh.GetCell(15, 588).WorldPosition.To2D(),
                NavMesh.GetCell(470, 588).WorldPosition.To2D(),
                NavMesh.GetCell(470, 530).WorldPosition.To2D(),
                NavMesh.GetCell(173, 526).WorldPosition.To2D(),
                NavMesh.GetCell(127, 510).WorldPosition.To2D(),
                NavMesh.GetCell(83, 464).WorldPosition.To2D(),
                NavMesh.GetCell(73, 444).WorldPosition.To2D(),
                NavMesh.GetCell(67, 192).WorldPosition.To2D(),
                NavMesh.GetCell(67, 120).WorldPosition.To2D(),
                NavMesh.GetCell(15, 120).WorldPosition.To2D()
            }
        };

        private static readonly Geometry.Polygon MidLanePos = new Geometry.Polygon
        {
            Points =
            {
                NavMesh.GetCell(125, 110).WorldPosition.To2D(),
                NavMesh.GetCell(105, 130).WorldPosition.To2D(),
                NavMesh.GetCell(150, 176).WorldPosition.To2D(),
                NavMesh.GetCell(418, 445).WorldPosition.To2D(),
                NavMesh.GetCell(455, 490).WorldPosition.To2D(),
                NavMesh.GetCell(490, 460).WorldPosition.To2D(),
                NavMesh.GetCell(441, 420).WorldPosition.To2D(),
                NavMesh.GetCell(177, 155).WorldPosition.To2D(),
                NavMesh.GetCell(125, 110).WorldPosition.To2D()
            }
        };

        private static readonly Geometry.Polygon BotLanePos = new Geometry.Polygon
        {
            Points =
            {
                NavMesh.GetCell(120, 15).WorldPosition.To2D(),
                NavMesh.GetCell(588, 15).WorldPosition.To2D(),
                NavMesh.GetCell(588, 470).WorldPosition.To2D(),
                NavMesh.GetCell(530, 470).WorldPosition.To2D(),
                NavMesh.GetCell(526, 173).WorldPosition.To2D(),
                NavMesh.GetCell(510, 127).WorldPosition.To2D(),
                NavMesh.GetCell(464, 83).WorldPosition.To2D(),
                NavMesh.GetCell(444, 73).WorldPosition.To2D(),
                NavMesh.GetCell(192, 67).WorldPosition.To2D(),
                NavMesh.GetCell(120, 67).WorldPosition.To2D(),
                NavMesh.GetCell(120, 15).WorldPosition.To2D()
            }
        };

        private static readonly Point MaxNavMeshCords = new Point(588, 593);

        private static readonly Geometry.Polygon ChaosBaseLanePos = new Geometry.Polygon
        {
            Points =
            {
                NavMesh.GetCell(MaxNavMeshCords.X - 0, MaxNavMeshCords.Y - 0).WorldPosition.To2D(),
                NavMesh.GetCell(MaxNavMeshCords.X - 0, MaxNavMeshCords.Y - 200).WorldPosition.To2D(),
                NavMesh.GetCell(MaxNavMeshCords.X - 105, MaxNavMeshCords.Y - 190).WorldPosition.To2D(),
                NavMesh.GetCell(MaxNavMeshCords.X - 160, MaxNavMeshCords.Y - 165).WorldPosition.To2D(),
                NavMesh.GetCell(MaxNavMeshCords.X - 190, MaxNavMeshCords.Y - 105).WorldPosition.To2D(),
                NavMesh.GetCell(MaxNavMeshCords.X - 200, MaxNavMeshCords.Y - 0).WorldPosition.To2D(),
                NavMesh.GetCell(MaxNavMeshCords.X - 0, MaxNavMeshCords.Y - 0).WorldPosition.To2D()
            }
        };

        private static readonly Geometry.Polygon OrderBaseLanePos = new Geometry.Polygon
        {
            Points =
            {
                NavMesh.GetCell(0, 0).WorldPosition.To2D(),
                NavMesh.GetCell(0, 200).WorldPosition.To2D(),
                NavMesh.GetCell(100, 200).WorldPosition.To2D(),
                NavMesh.GetCell(170, 170).WorldPosition.To2D(),
                NavMesh.GetCell(200, 100).WorldPosition.To2D(),
                NavMesh.GetCell(200, 0).WorldPosition.To2D(),
                NavMesh.GetCell(0, 0).WorldPosition.To2D()
            }
        };

        public static Lanes GetAlliedBase()
        {
            return Player.Instance.Team == GameObjectTeam.Order ? OrderBase : ChaosBase;
        }

        public static Lanes GetLane(this Obj_AI_Base item)
        {
            return item.Position.GetLane();
        }

        public static Lanes GetLane(this Vector3 pos)
        {
            if (ChaosBase.InLane(pos))
            {
                return ChaosBase;
            }
            if (OrderBase.InLane(pos))
            {
                return OrderBase;
            }
            if (Top.InLane(pos))
            {
                return Top;
            }
            if (Middle.InLane(pos))
            {
                return Middle;
            }
            if (Bottom.InLane(pos))
            {
                return Bottom;
            }

            return Jungle;
        }

        public static void DrawLane(this Lanes lane, Color color, int width = 1)
        {
            switch (lane)
            {
                case Lanes.Top:
                    TopLanePos.Draw(color, width);
                    break;
                case Lanes.Middle:
                    MidLanePos.Draw(color, width);
                    break;
                case Lanes.Bottom:
                    BotLanePos.Draw(color, width);
                    break;
                case Lanes.ChaosBase:
                    ChaosBaseLanePos.Draw(color, width);
                    break;
                case Lanes.OrderBase:
                    OrderBaseLanePos.Draw(color, width);
                    break;
            }
        }

        public static void DrawLaneMiniMap(this Lanes lane, Color color, int width = 1)
        {
            switch (lane)
            {
                case Lanes.Top:
                    var pointst = TopLanePos.Points.Select(x => x.To3D().WorldToMinimap());
                    Line.DrawLine(color, width, pointst.ToArray());
                    break;
                case Lanes.Middle:
                    var pointsm = MidLanePos.Points.Select(x => x.To3D().WorldToMinimap());
                    Line.DrawLine(color, width, pointsm.ToArray());
                    break;
                case Lanes.Bottom:
                    var pointsb = BotLanePos.Points.Select(x => x.To3D().WorldToMinimap());
                    Line.DrawLine(color, width, pointsb.ToArray());
                    break;
                case Lanes.ChaosBase:
                    var pointscb = ChaosBaseLanePos.Points.Select(x => x.To3D().WorldToMinimap());
                    Line.DrawLine(color, width, pointscb.ToArray());
                    break;
                case Lanes.OrderBase:
                    var pointsob = OrderBaseLanePos.Points.Select(x => x.To3D().WorldToMinimap());
                    Line.DrawLine(color, width, pointsob.ToArray());
                    break;
            }
        }

        public static bool InLane(this Obj_AI_Base obj, Lanes lane)
        {
            return InLane(lane, obj.Position);
        }

        public static bool InLane(this Lanes lane, Vector3 pos)
        {
            switch (lane)
            {
                case Lanes.ChaosBase:
                    return ChaosBaseLanePos.IsInside(pos);
                case Lanes.OrderBase:
                    return OrderBaseLanePos.IsInside(pos);
                case Lanes.Top:
                    return TopLanePos.IsInside(pos);
                case Lanes.Middle:
                    return MidLanePos.IsInside(pos);
                case Lanes.Bottom:
                    return BotLanePos.IsInside(pos);
                case Lanes.Jungle:
                    return !InLane(Top, pos) && !InLane(Middle, pos) && !InLane(Bottom, pos);
            }
            return false;
        }
    }
}