using System;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AutoRift.Helpers
{
    public static partial class HelperExtentions
    {
        public static Random Random = new Random(Convert.ToInt32(Game.GameId) + DateTime.Now.Millisecond);

        public static int Randomize(this int maxDelay, int min = 0)
        {
            return Random.Next(min, maxDelay);
        }
        public static Vector3 Randomize(this Vector3 pos, float maxDistance)
        {
            var point =
                pos.Extend(
                    Random.NextVector3(pos.Offset(-maxDistance, -maxDistance), pos.Offset(maxDistance, maxDistance)),
                    maxDistance);
            return point.To3DWorld();
        }
    }
}