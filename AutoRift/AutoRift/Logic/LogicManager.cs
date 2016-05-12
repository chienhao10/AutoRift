using System;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AutoRift.Logic
{
    public static class LogicManager
    {
        public static ILogic Logic { get; set; }
        public static Vector3 CurrentOrbwalkLocation { get; set; }
        public static bool OverideOrbwalkerEnabled { get; set; } = true;
        public static void Init()
        {
            Game.OnUpdate += Game_OnUpdate;
            Orbwalker.OverrideOrbwalkPosition = OverrideOrbwalkPosition;
            LogicSelector.AutoSelectLogic();
            //EventManager.OnGameStart += a => Logic.Start();
        }

        private static Vector3? OverrideOrbwalkPosition()
        {
            if (OverideOrbwalkerEnabled)
            {
                return CurrentOrbwalkLocation;
            }
            return null;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            var movementData = Logic.GetMovementData();
            if (movementData != null)
            {
                
                Orbwalker.ActiveModesFlags = movementData.OrwalkerModes;
                CurrentOrbwalkLocation = movementData.Posistion;
            }
            else
            {
                Orbwalker.ActiveModesFlags = Orbwalker.ActiveModes.None;
                CurrentOrbwalkLocation = Game.CursorPos;
            }
            LogicSelector.AutoSelectLogic();
            Logic.Update();
        }
    }
}