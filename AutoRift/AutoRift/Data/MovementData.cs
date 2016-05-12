using EloBuddy.SDK;
using SharpDX;

namespace AutoRift.Data
{
    public class MovementData
    {
        public MovementData(Vector3 posistion, Orbwalker.ActiveModes orwalkerModes)
        {
            Posistion = posistion;
            OrwalkerModes = orwalkerModes;
        }

        public Vector3 Posistion { get; set; }
        public Orbwalker.ActiveModes OrwalkerModes { get; set; }
    }
}