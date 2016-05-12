using System.Collections.Generic;
using System.Linq;
using EloBuddy.SDK;
using SharpDX;

namespace AutoRift.Helpers
{
    public static partial class HelperExtentions
    {
        public static Vector2 Offset(this Vector2 value, float? x = null, float? y = null)
        {
            if (x.HasValue)
            {
                value.X += x.Value;
            }
            if (y.HasValue)
            {
                value.Y += y.Value;
            }
            return value;
        }

        public static Vector3 Offset(this Vector3 value, float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue)
            {
                value.X += x.Value;
            }
            if (y.HasValue)
            {
                value.Y += y.Value;
            }
            if (z.HasValue)
            {
                value.Z += z.Value;
            }
            return value;
        }

        public static Vector2 Override(this Vector2 value, float? x = null, float? y = null)
        {
            if (x.HasValue)
            {
                value.X = x.Value;
            }
            if (y.HasValue)
            {
                value.Y = y.Value;
            }
            return value;
        }

        public static Vector3 Override(this Vector3 value, float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue)
            {
                value.X = x.Value;
            }
            if (y.HasValue)
            {
                value.Y = y.Value;
            }
            if (z.HasValue)
            {
                value.Z = z.Value;
            }
            return value;
        }

        public static float Distance(this IEnumerable<Vector3> vectors)
        {
            var array = vectors.ToArray();
            var length = 0f;
            for (int index = 1; index < array.Length; index++)
            {
                length += array[index].Distance(array[index - 1]);
            }
            return length;
        }

        public static Vector3 Average(this IEnumerable<Vector3> vectors)
        {
            var list = vectors.ToList();
            return new Vector3(list.Average(x => x.X), list.Average(x => x.Y), list.Average(x => x.Z));
        }
    }
}