using System;
using System.Linq;
using System.Text;
using AutoRift.Data;
using AutoRift.Helpers;
using AutoRift.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace AutoRift
{
    public static class Global
    {
        private const int LaneSelectedWidth = 3;
        private const int LaneDefaultWidth = 1;
        private static readonly Color LaneSelectedColor = Color.Green;
        private static Lane.Lanes _selectedLane;
        public static ItemBuild ItemBuild { get; set; }
        public static SpellBuild SpellBuild { get; set; }

        public static Lane.Lanes SelectedLane
        {
            get { return _selectedLane; }
            set
            {
                _selectedLane = value;
                if (_selectedLane != value)
                    EventManager.OnTakeNewLaneHandler(value);
            }
        }

        public static Lane.Lanes CurrentLane
        {
            get { return Player.Instance.Position.GetLane(); }
        }

        public static void Init()
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            DataManager.Init();
            MinionManager.Init();
            var itembuilds = BuildManager.Builds[Player.Instance.Hero].ItemBuilds;
            var spellbuilds = BuildManager.Builds[Player.Instance.Hero].SpellBuilds;
            ItemBuild = itembuilds[HelperExtentions.Random.Next(itembuilds.Count - 1)];
            SpellBuild = spellbuilds[HelperExtentions.Random.Next(spellbuilds.Count - 1)];

            SelectedLane = Lane.GetAlliedBase();
            Config.Init();
            EventManager.Init();
            LogicManager.Init();
            EventManager.OnLevelUp += PlayerLevelUp;
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
        }

        private static void PlayerLevelUp(AIHeroClient sender)
        {
            Core.DelayAction(LevelUp, 400.Randomize());
        }

        public static void LevelUp()
        {
            var playerLevelIndex = Player.Instance.Level - 1;
            Player.LevelSpell(SpellBuild.ElementAt(playerLevelIndex));
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (ConfigKey.DrawOrbwalkPosistion.Bool())
                Circle.Draw(new ColorBGRA(Color.Cornsilk.R, Color.Cornsilk.G, Color.Cornsilk.B, 255), 60,
                    LogicManager.CurrentOrbwalkLocation);
            if (ConfigKey.DrawPlayerPaths.Bool())
            {
                foreach (var hero in EntityManager.Heroes.AllHeroes.Where(x => x.IsHPBarRendered || x.Path.Any(b => b.IsOnScreen())))
                {
                    var color = hero.IsAlly ? Color.DarkGreen : Color.OrangeRed;
                    Line.DrawLine(color, 1, hero.Path.Where(x => x.IsOnScreen()).ToArray());
                }
            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (ConfigKey.DrawLanesOnMiniMap.Bool())
            {
                Lane.Top.DrawLaneMiniMap(SelectedLane == Lane.Top ? LaneSelectedColor : Color.White,
                    CurrentLane == Lane.Top ? LaneSelectedWidth : LaneDefaultWidth);
                Lane.Bottom.DrawLaneMiniMap(SelectedLane == Lane.Bottom ? LaneSelectedColor : Color.White,
                    CurrentLane == Lane.Bottom ? LaneSelectedWidth : LaneDefaultWidth);
                Lane.Middle.DrawLaneMiniMap(SelectedLane == Lane.Middle ? LaneSelectedColor : Color.White,
                    CurrentLane == Lane.Middle ? LaneSelectedWidth : LaneDefaultWidth);
                Lane.OrderBase.DrawLaneMiniMap(SelectedLane == Lane.OrderBase ? LaneSelectedColor : Color.DeepSkyBlue,
                    CurrentLane == Lane.OrderBase ? LaneSelectedWidth : LaneDefaultWidth);
                Lane.ChaosBase.DrawLaneMiniMap(SelectedLane == Lane.ChaosBase ? LaneSelectedColor : Color.OrangeRed,
                    CurrentLane == Lane.ChaosBase ? LaneSelectedWidth : LaneDefaultWidth);
            }
            if (ConfigKey.DrawPlayerPathsOnMiniMap.Bool())
            {
                foreach (var hero in EntityManager.Heroes.AllHeroes.Where(x => !x.IsDead))
                {
                    var pathMiniMap = hero.Path.Select(x => x.WorldToMinimap());
                    var color = hero.IsAlly ? Color.DarkGreen : Color.OrangeRed;
                    Line.DrawLine(color, 1, pathMiniMap.ToArray());
                }
            }
            if (ConfigKey.DrawLogicStatus.Bool())
            {
                var builder = new StringBuilder();
                builder.AppendLine($"Selected Logic:  {LogicManager.Logic.GetType().Name}");
                builder.AppendLine("Logic Messages:");
                foreach (var statusMessage in LogicManager.Logic.StatusMessages())
                {
                    builder.AppendLine(statusMessage);
                }
                Drawing.DrawText(100, 10, Color.White, builder.ToString());
            }
            
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            
        }


        public static void BuyItems(ItemBuild build = null, params Tuple<int, ItemId>[] stock)
        {
            if (build == null)
            {
                build = ItemBuild;
            }
            if (!Player.Instance.IsInShopRange())
            {
                return;
            }
            if (stock != null && stock.Length > 0)
            {
                bool fullyStocked = false;
                while (!fullyStocked)
                {
                    foreach (var item in stock)
                    {
                        if (Player.Instance.InventoryItems.Any(x => x.Id == item.Item2 && x.Charges >= item.Item1))
                        {
                            fullyStocked = true;
                            continue; //Player already has item and is correct amount
                        }
                        if (BuyItem(item.Item2)) {
                            fullyStocked = false; //We correctly bought the item, but that does not mean we cant buy more, repeat.
}
                        else fullyStocked = true; //We couldn't buy the item for some reason, we are either fully stocked, or we do not have enough gold.
                    }
                    
                }
                
            }
            foreach (var item in build)
            {
                if (Player.Instance.InventoryItems.Any(x => x.Id == item)) {
                    continue; //Player already has item.
}
                if (BuyItem(item))
                {
                    continue;
                }
                if (!Item.ItemData.ContainsKey(item))
                {
                    continue; // SDK does not have data for the item, so lets continue to the next item...
                }
                var itemData = Item.ItemData[item];
                //Try build item seeing we cant buy it outright
                foreach (var itemId in itemData.IntoId)
                {
                    if (BuyItem(itemId))
                    {
                        //TODO: Logic if we cant buy item
                        continue;
                    }
                }
            }
        }

        public static bool BuyItem(ItemId item)
        {
            if (!Item.ItemData.ContainsKey(item))
            {
                return false;
            }
            var itemData = Item.ItemData[item];
            if (itemData.Gold.Total <= Player.Instance.Gold)
            {
                return Shop.BuyItem(item);
            }
            return false;
        }
    }

}
