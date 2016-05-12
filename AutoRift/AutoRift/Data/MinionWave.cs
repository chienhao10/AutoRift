using System.Collections.Generic;
using System.Linq;
using AutoRift.Helpers;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace AutoRift.Data
{
    public class MinionWave
    {
        public MinionWave(Lane.Lanes lane, GameObjectTeam team)
        {
            Team = team;
            WaveLane = lane;
            TimeCreated = Game.Time;
            Wave = new List<Obj_AI_Minion>();
        }

        public int Id { get; set; }
        public Vector3 CenterOfPolygon { get; set; }
        public List<Obj_AI_Minion> Wave { get; set; }
        public float TimeCreated { get; set; }

        public Lane.Lanes WaveLane { get; set; }
        public GameObjectTeam Team { get; set; }
        public bool IsAlly => Player.Instance.Team == Team;
        public bool IsEnemy => !IsAlly;

        public bool IsWaveDead => IsLaneWaveDeadCheck();
        public bool IsWaveFullySpawned => IsWaveFullySpawnedCheck();
        public bool IsWaveGrouped => IsGroupedCheck();

        public void Update()
        {
            Wave.RemoveAll(x => x == null || x.IsDead);
            CenterOfPolygon = Wave.Select(x => x.Position).Average();
        }
        public void Draw()
        {
            foreach (var minion in Wave)
            {
                Drawing.DrawText(minion.HPBarPosition.Offset(30, -15), IsAlly ? Color.DodgerBlue : Color.OrangeRed, $"WID: {Id}", 8);
            }
            if (IsAlly)
            {
                Circle.Draw(new ColorBGRA(255, 255, 255, 255), 10, CenterOfPolygon);
            }
        }

        private bool IsLaneWaveDeadCheck()
        {
            return Wave.All(x => x == null || x.IsDead);
        }

        private bool IsGroupedCheck()
        {
            if (Wave.Count < 2)
            {
                return true;
            }
            var firstMinion = Wave.First();
            return Wave.All(x => x.Position.IsInRange(firstMinion, 1500));
        }

        private bool IsWaveFullySpawnedCheck()
        {
            if (IsWaveDead)
            {
                return false;
            }
            var containsSiegeMinion = Wave.Any(x => x.Model.Contains("Siege"));
            return (containsSiegeMinion && Wave.Count == 7) || (!containsSiegeMinion && Wave.Count == 6);
        }
    }
}