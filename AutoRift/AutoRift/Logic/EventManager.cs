using System;
using System.Collections.Generic;
using System.Linq;
using AutoRift.Data;
using AutoRift.Data.Events;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

namespace AutoRift.Logic
{
    public static class EventManager
    {
        private static bool _calledOnArriveAtSpawn;
        private static readonly Dictionary<AIHeroClient, bool> CalledLaneTaken = new Dictionary<AIHeroClient, bool>();

        private static readonly Dictionary<Obj_AI_Turret, List<Obj_AI_Base>> TurrentAttackers =
            new Dictionary<Obj_AI_Turret, List<Obj_AI_Base>>();

        /// <summary>
        ///     Invoked when the game starts, or the player re-spawns from death.
        /// </summary>
        public static event EventManagerDelegates.OnSpawn OnGameStart;

        /// <summary>
        ///     Invoked when the player arrives at the spawn point, either from recall or walking. (In Shop Range)
        /// </summary>
        public static event EventManagerDelegates.OnArriveAtSpawn OnArriveAtSpawn;

        /// <summary>
        ///     Invoked when a player takes a lane (walked into the lane from another, or base)
        /// </summary>
        public static event EventManagerDelegates.OnLaneTaken OnLaneTaken;

        public static event EventManagerDelegates.OnTakeNewLane OnTakeNewLane;
        public static event EventManagerDelegates.OnAlliedTurrentDamage OnAlliedTurrentDamage;
        public static event EventManagerDelegates.OnLevelUp OnLevelUp;

        /// <summary>
        /// When the entire wave has spawned
        /// </summary>
        public static event EventManagerDelegates.OnWaveSpawn OnWaveSpawned;
        /// <summary>
        /// When the first minion of a wave is spawned
        /// </summary>
        public static event EventManagerDelegates.OnWaveSpawn OnWaveSpawn;

        public static void Init()
        {
            EntityManager.Heroes.AllHeroes.ForEach(x => CalledLaneTaken.Add(x, false));
            ObjectManager.Get<Obj_AI_Turret>().Where(x => x.IsAlly).ToList().ForEach(x => TurrentAttackers.Add(x, null));
            Core.DelayAction(CallOnSpawn, 500);
            
            Game.OnUpdate += Game_OnUpdate;
            AttackableUnit.OnDamage += Obj_AI_Base_OnDamage;
            Obj_AI_Base.OnLevelUp += Obj_AI_Base_OnLevelUp;
            Teleport.OnTeleport += Teleport_OnTeleport;
            Obj_AI_Base.OnNewPath += AIHeroClient_OnNewPath;
        }

        private static void Obj_AI_Base_OnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            var item = sender as AIHeroClient;
            if (item != null && item.IsMe && item.Level <= 18)
            {
                OnLevelUp?.Invoke(item);
            }
        }

        private static void Obj_AI_Base_OnDamage(AttackableUnit sender, AttackableUnitDamageEventArgs args)
        {
            var turrent = args.Target as Obj_AI_Turret;
            if (turrent != null && (args.Source is AIHeroClient || args.Source is Obj_AI_Minion))
            {
                if (!TurrentAttackers.ContainsKey(turrent))
                {
                    TurrentAttackers.Add(turrent, new List<Obj_AI_Base>());
                }

                TurrentAttackers[turrent]
                    .RemoveAll(x => x.IsDead || !x.IsInRange(args.Target, x.AttackRange)); //Remove all objects that could not be attacking turret
                var list = TurrentAttackers[turrent]; // Collect the list for this turret
                if (!list.Contains((Obj_AI_Base) sender)) // Make sure that the current attacker has not already been added.
                {
                    list.Add((Obj_AI_Base) sender);
                    OnAlliedTurrentDamage?.Invoke(new TurrentDamageEventArgs(turrent,
                        list.ToArray(),
                        turrent.Position.GetLane()));
                }
            }
        }

        private static void AIHeroClient_OnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            var heroClient = sender as AIHeroClient;
            if (heroClient == null) return;

            var lane = args.Path.Last().GetLane();
            var playerLane = heroClient.Position.GetLane();
            //If player is in the lane, and moving around in it, they have "taken" that lane.
            if (lane == playerLane && !CalledLaneTaken[heroClient])
            {
                var arg = new LaneTakenEventArgs(heroClient, lane);
                OnLaneTaken?.Invoke(arg);
                if (arg.TakeLane != Global.CurrentLane && arg.TakeLane != Global.SelectedLane)
                {
                    //Player has stated that it wants to move to a new lane, so lets call the event to signal that we are trying to change lanes.
                    Global.SelectedLane = arg.TakeLane;
                }
            }
        }

        private static void Teleport_OnTeleport(Obj_AI_Base sender, Teleport.TeleportEventArgs args)
        {
            if (sender is AIHeroClient && sender.IsMe && args.Status == TeleportStatus.Finish)
            {
                //Player has just recalled, so we must be at the shop.
                CallOnArriveAtSpawn();
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.IsInShopRange())
            {
                CallOnArriveAtSpawn();
                
            }

            if (!Player.Instance.IsInShopRange())
            {
                _calledOnArriveAtSpawn = false;
                
            }
        }

        private static void CallOnSpawn()
        {
            var spawnArgs = new OnSpawnEventArgs(Player.Instance);
            if (Player.Instance.IsInShopRange() && EntityManager.Heroes.AllHeroes.All(x => x.Level > 0))
            {
                OnGameStart?.Invoke(spawnArgs);
            }
        }

        public static void OnMinionWaveSpawn(MinionWave wave)
        {
            if (wave == null) return;
            OnWaveSpawn?.Invoke(wave);
        }
        public static void OnMinionWaveSpawned(MinionWave wave)
        {
            if(wave == null) return;
            OnWaveSpawned?.Invoke(wave);
        }

        private static void CallOnArriveAtSpawn()
        {
            if (!_calledOnArriveAtSpawn)
            {
                _calledOnArriveAtSpawn = true;
                var args = new OnSpawnEventArgs(Player.Instance) {Lane = Global.SelectedLane};
                OnArriveAtSpawn?.Invoke(args);
            }
        }

        public static void OnTakeNewLaneHandler(Lane.Lanes value)
        {
            OnTakeNewLane?.Invoke(value);
        }
    }
}