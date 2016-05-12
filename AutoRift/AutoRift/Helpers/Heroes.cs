using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AutoRift.Helpers
{
    public static class Heroes
    {
        public static bool IsAnyInRange(this IEnumerable<AIHeroClient> heros, Vector3 pos, float range, bool ignoreMe = true, bool ignoreDead = true)
        {
            return heros.Any(x => x.IsInRange(pos, range) && (!ignoreMe || x.IsMe) && (!ignoreDead || x.IsDead));
        }

        public static bool IsNoneInRange(this IEnumerable<AIHeroClient> heros, Vector3 pos, float range, bool ignoreMe = true, bool ignoreDead = true)
        {
            return !IsAnyInRange(heros, pos, range, ignoreMe, ignoreDead);
        }

    }
}