using System;
using System.Collections.Generic;
using System.Linq;
using AutoRift.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace AutoRift.Data
{
    public static class MinionManager
    {
        public static Dictionary<Lane.Lanes, Obj_Barracks> SpawnPoints { get; set; }
        public static List<MinionWave> MinionWaves { get; set; }
        private static Dictionary<Lane.Lanes, float> LastSpawnedTime { get; set; }
        private static int _lastId = 0;
        public static void Init()
        {
            LastSpawnedTime = new Dictionary<Lane.Lanes, float>();
            SpawnPoints = new Dictionary<Lane.Lanes, Obj_Barracks>();
            foreach (var spawnPoint in ObjectManager.Get<Obj_Barracks>())
            {
                var lane = spawnPoint.Name.EndsWith("C01")
                    ? Lane.Middle
                    : (spawnPoint.Name.EndsWith("L01") ? Lane.Top : Lane.Bottom);
                SpawnPoints[lane] = spawnPoint;
            }
            MinionWaves = new List<MinionWave>();
            Game.OnUpdate += Game_OnUpdate;
            GameObject.OnCreate += GameObject_OnCreate;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            MinionWaves.RemoveAll(x => x.IsWaveDead);
            foreach (var minionWave in MinionWaves)
            {
                minionWave.Update();
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            Drawing.DrawText(0, 0, Color.Yellow, "Test!");
            Circle.Draw(new ColorBGRA(255, 255, 255, 255), 20, SpawnPoints.Values.Cast<GameObject>().ToArray());

            var drawingState = (MinionWaveDrawing) ConfigKey.DrawMinionWaveBoundaries.Int();
            foreach (var minionWave in MinionWaves.Where(x => CheckIfDrawWave(x, drawingState)))
            {
                minionWave.Draw();
            }
        }

        private static bool CheckIfDrawWave(MinionWave x, MinionWaveDrawing drawingState)
        {
            switch (drawingState)
            {
                case MinionWaveDrawing.All:
                    return true;
                case MinionWaveDrawing.Ally:
                    return x.IsAlly;
                case MinionWaveDrawing.Enemy:
                    return x.IsEnemy;
                default:
                    return false;
            }
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (!(sender is Obj_AI_Minion) ||
                !SpawnPoints.Values.Any(x => x.Position.IsInRange(sender, sender.BoundingRadius)))
            {
                return;
            }

            var barrack = SpawnPoints.First(x => x.Value.Position.IsInRange(sender, sender.BoundingRadius));
            var lastWave = MinionWaves.Count(x => x.WaveLane == barrack.Key && x.Team == barrack.Value.Team) > 0 ? MinionWaves.Last(x => x.WaveLane == barrack.Key && x.Team == barrack.Value.Team) : null;

            if (lastWave == null || LastSpawnedTime[barrack.Key] < Game.Time - 6 || lastWave.IsWaveFullySpawned)
            {
                lastWave = new MinionWave(barrack.Key, sender.Team)
                {
                    Id = _lastId++
                };
                lastWave.Wave.Add((Obj_AI_Minion) sender);
                MinionWaves.Add(lastWave);
                EventManager.OnMinionWaveSpawn(lastWave);
            }
            else
            {
                lastWave.Wave.Add((Obj_AI_Minion) sender);
            }

            if (lastWave.IsWaveFullySpawned)
            {
                EventManager.OnMinionWaveSpawned(lastWave);
            }
            LastSpawnedTime[barrack.Key] = Game.Time;
        }

        public enum MinionWaveDrawing
        {
            All,
            Ally,
            Enemy,
            None
        }
    }
}