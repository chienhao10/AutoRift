using System;
using System.Linq;
using AutoRift.Data;
using AutoRift.Helpers;
using EloBuddy;
using EloBuddy.SDK;

namespace AutoRift.Logic
{
    public class LogicSelector
    {
        public const int NearEnemyRange = 1750;
        public const int NearAllyRange = 2500;
        public const float FleeHealthPercent = 0.25f;
        public static ILogic AutoSelectLogic(bool setLogic = true)
        {
            return LogicManager.Logic == null ? NewLogicCheck(setLogic) : CurrentLogicChecks(setLogic);
        }

        private static ILogic CurrentLogicChecks(bool setLogic = true)
        {

            #region Death Logics

            if (Player.Instance.IsDead)
            {
                return ChangeLogic<DefaultDeathLogic>();
            }
            if (LogicManager.Logic is DefaultDeathLogic)
            {
                if (!Player.Instance.IsDead && Player.Instance.IsInShopRange())
                {
                    return ChangeLogic<DefaultLaneSelectLogic>();
                }
            }
            #endregion

            if (LogicManager.Logic is DefaultLaneSelectLogic)
            {
                if (LogicManager.Logic.Finished())
                {
                    return ChangeLogic<DefaultFarmLogic>(setLogic); //We have selected a lane, so lets start farming
                }
                return LogicManager.Logic; // Haven't finished selecting a lane, so keep waiting
            }

            if (LogicManager.Logic is DefaultFarmLogic)
            {
                if (EntityManager.Heroes.Enemies.IsAnyInRange(Player.Instance.Position, Player.Instance.AttackRange))
                {
                    if (Player.Instance.HealthPercent <= FleeHealthPercent)
                    {
                        return ChangeLogic<DefaultFleeLogic>();
                    }

                    if (EntityManager.Heroes.Allies.IsAnyInRange(Player.Instance.Position, NearAllyRange))
                    {
                        return ChangeLogic<DefaultFightLogic>(setLogic);
                    }

                    return ChangeLogic<DefaultHarrasLogic>(setLogic);
                }
            }

            if (LogicManager.Logic is DefaultHarrasLogic)
            {
                if (EntityManager.Heroes.Enemies.IsNoneInRange(Player.Instance.Position, NearEnemyRange))
                {
                    return ChangeLogic<DefaultFarmLogic>(setLogic);
                }
            }
            if (LogicManager.Logic is DefaultFightLogic)
            {
                if (!EntityManager.Heroes.Enemies.IsAnyInRange(Player.Instance.Position, NearEnemyRange))
                {
                    return ChangeLogic<DefaultFarmLogic>(setLogic);
                }
            }
            if (LogicManager.Logic is DefaultFleeLogic)
            {
                if (Player.Instance.HealthPercent > FleeHealthPercent &&
                    EntityManager.Heroes.Enemies.IsNoneInRange(Player.Instance.Position, NearEnemyRange))
                {
                    return ChangeLogic<DefaultFarmLogic>();
                }
            }
            if (Player.Instance.IsInShopRange())
            {
                return ChangeLogic<DefaultLaneSelectLogic>();
            }
            
            return LogicManager.Logic;
        }

        private static ILogic NewLogicCheck(bool setLogic = true)
        {
            if (Player.Instance.IsDead)
            {
                return ChangeLogic<DefaultDeathLogic>();
            }
            if (Player.Instance.HealthPercent <= FleeHealthPercent) return ChangeLogic<DefaultFleeLogic>();

            if (Player.Instance.IsInShopRange())
            {
                // Player is at shop, we need to select a lane again.
                return ChangeLogic<DefaultLaneSelectLogic>(setLogic);
            }
            return ChangeLogic<DefaultFarmLogic>();


        }

        public static ILogic ChangeLogic<T>(bool setValue = true) where T : ILogic
        {
            if (LogicManager.Logic is T)
            {
                return LogicManager.Logic;
                
            }
            var newLogic = (ILogic) Activator.CreateInstance(typeof (T));
            newLogic.Init();
            if (setValue)
            {
                Console.WriteLine("Changing Logic to: " + newLogic.GetType().Name);
                if(LogicManager.Logic != null) LogicManager.Logic.End();
                LogicManager.Logic = newLogic;
                LogicManager.Logic.Start();
            }
            return newLogic;
        }
    }
}