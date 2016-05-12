using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AutoRift.Data
{
    public static class Turrent
    {
        public enum Tier
        {
            Outter,
            Inner,
            Inhibitor,
            Nexus,
            Spawn
        }

        private const string SpawnTurrent = "Order5";
        private const string NexusTurrent = "Order4";
        private const string InhibitorTurrent = "Order3";
        private const string InnerTurrent = "Order2";
        private const string OutterTurrent = "Order1";

        public static readonly float TurrentsRange = 780 + Player.Instance.BoundingRadius;

        public static List<Obj_AI_Turret> AllyTurrents
        {
            get { return EntityManager.Turrets.Allies; }
        }

        public static List<Obj_AI_Turret> EnemyTurrents
        {
            get { return EntityManager.Turrets.Enemies; }
        }

        public static Tier TurrentTier(this Obj_AI_Turret turrent)
        {
            if (turrent.BaseSkinName.EndsWith(SpawnTurrent)) return Tier.Spawn;
            if (turrent.BaseSkinName.EndsWith(NexusTurrent)) return Tier.Nexus;
            if (turrent.BaseSkinName.EndsWith(InhibitorTurrent)) return Tier.Inhibitor;
            if (turrent.BaseSkinName.EndsWith(InnerTurrent)) return Tier.Inner;
            return Tier.Outter;
        }

        //Is Near Turrents Range
        public static bool IsNearAllyTurrent(this Vector3 pos, float range, params Tier[] tiers)
        {
            return
                AllyTurrents.Any(
                    x =>
                        x.IsInRange(pos, TurrentsRange + range) &&
                        (tiers == null || tiers.Any(i => i == x.TurrentTier())));
        }

        public static bool IsNearEnemyTurrent(this Vector3 pos, float range, params Tier[] tiers)
        {
            return
                EnemyTurrents.Any(
                    x =>
                        x.IsInRange(pos, TurrentsRange + range) &&
                        (tiers == null || tiers.Any(i => i == x.TurrentTier())));
        }

        public static bool IsNearTurrent(this Vector3 pos, float range, params Tier[] tiers)
        {
            return IsNearAllyTurrent(pos, range, tiers) || IsNearEnemyTurrent(pos, range, tiers);
        }

        public static Obj_AI_Turret GetLaneTurret(Func<Obj_AI_Turret, bool> selector)
        {
            var candidates = AllyTurrents.Where(selector.Invoke).ToList();
            var turret = ((candidates.FirstOrDefault(x => x.TurrentTier() == Tier.Outter) ??
                           candidates.FirstOrDefault(x => x.TurrentTier() == Tier.Inner)) ??
                          candidates.FirstOrDefault(x => x.TurrentTier() == Tier.Inhibitor)) ??
                         AllyTurrents.FirstOrDefault(x => x.TurrentTier() == Tier.Nexus);
            return turret;
        }

        /// <summary>
        ///     Is In Range of Turret
        /// </summary>
        public static bool IsInTurrent(this Vector3 pos, params Tier[] tiers)
        {
            return IsInAllyTurrent(pos, tiers) || IsInEnemyTurrent(pos, tiers);
        }

        /// <summary>
        ///     Is in attack range of allied turret
        /// </summary>
        public static bool IsInAllyTurrent(this Vector3 pos, params Tier[] tiers)
        {
            //Turret is in range, and if tiers specified is it that tier
            return
                AllyTurrents.Any(
                    x => x.IsInRange(pos, TurrentsRange) && (tiers == null || tiers.Any(i => i == x.TurrentTier())));
        }

        /// <summary>
        ///     Is in attack range of enemy turret
        /// </summary>
        public static bool IsInEnemyTurrent(this Vector3 pos, params Tier[] tiers)
        {
            //Turret is in range, and if tiers specified is it that tier
            return
                EnemyTurrents.Any(
                    x => x.IsInRange(pos, TurrentsRange) && (tiers == null || tiers.Any(i => i == x.TurrentTier())));
        }
    }
}