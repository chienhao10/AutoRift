using System;
using System.CodeDom;
using AutoRift.Data;
using AutoRift.Properties;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AutoRift
{
    public static class MenuManager
    {
        public static Menu MainMenu { get; private set; }

        public static Menu LogicsMenu { get; private set; }
        public static Menu DrawingsMenu { get; private set; }

        public static Menu Credits { get; private set; }
        public static Menu TermsOfUse { get; private set; }

        public static void LoadMenu()
        {
            MainMenu = EloBuddy.SDK.Menu.MainMenu.AddMenu("AutoRift", "AutoRift",
                "AutoRift - Complex Artificial Intelligence for League of Legends");
            LogicsMenu = MainMenu.AddSubMenu("Logics");
            DrawingsMenu = MainMenu.AddSubMenu("Drawings");
            Credits = MainMenu.AddSubMenu("Credits");
            Credits.AddGroupLabel("Credits");
            Credits.AddSeparator(50);
            Credits.AddLabel(Resources.Credits);
            TermsOfUse = MainMenu.AddSubMenu("Terms Of Use");
            TermsOfUse.AddGroupLabel("Terms Of Use");
            TermsOfUse.AddSeparator(50);
            TermsOfUse.AddLabel(Resources.TermsOfUse);

            Config.Set(DrawingsMenu, ConfigKey.DrawLanesOnMiniMap, new CheckBox("Draw Lane Border on Mini-Map"));
            DrawingsMenu.AddSeparator();
            Config.Set(DrawingsMenu, ConfigKey.DrawPlayerPathsOnMiniMap, new CheckBox("Draw Player Paths On Mini-Map"));
            Config.Set(DrawingsMenu, ConfigKey.DrawPlayerPaths, new CheckBox("Draw Player Paths On World"));
            Config.Set(DrawingsMenu, ConfigKey.DrawMinionWaveBoundaries, new ComboBox("Minion Wave Drawing", (int)MinionManager.MinionWaveDrawing.All, Enum.GetNames(typeof(MinionManager.MinionWaveDrawing))));
            Config.Set(DrawingsMenu, ConfigKey.DrawOrbwalkPosistion, new CheckBox("Draw Orbwalking Position"));
            Config.Set(DrawingsMenu, ConfigKey.DrawLogicStatus, new CheckBox("Draw Logic Status"));


            MainMenu.AddLabel("If 'Force Lane' is unchecked, and we can not find an empty lane, we will use this value anyway.");
            Config.Set(MainMenu, ConfigKey.ForceLane, new CheckBox("Force Lane", false));
            Config.Set(MainMenu, ConfigKey.ForceLaneValue, new ComboBox("Lane", (int)Lane.Lanes.Middle, Enum.GetNames(typeof(Lane.Lanes)))).OnValueChange += ForcedLaneValue_Changed; ;
            
            
        }

        private static void ForcedLaneValue_Changed(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            var newLane = (Lane.Lanes)args.NewValue;
            if (newLane == Lane.Lanes.ChaosBase || newLane == Lane.Lanes.OrderBase || newLane == Lane.Lanes.Jungle)
            {
                sender.CurrentValue = (int) Lane.Lanes.Middle;
            }
        }
    }
}