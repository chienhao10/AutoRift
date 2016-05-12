using EloBuddy;

namespace AutoRift.Data
{
    public static class Nexus
    {
        public static Obj_HQ Enemy { get; set; }
        public static Obj_HQ Ally { get; set; }

        static Nexus()
        {
            foreach (var type in ObjectManager.Get<Obj_HQ>())
            {
                if (type.IsEnemy)
                {
                    Enemy = type;
                }
                if (type.IsAlly)
                {
                    Ally = type;
                }

            }
        }
    }
}