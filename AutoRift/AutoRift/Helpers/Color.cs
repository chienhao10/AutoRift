using SharpDX;

namespace AutoRift.Helpers
{
    public static class Color
    {
        public static ColorBGRA ToBgraColor(this System.Drawing.Color color)
        {
            return new ColorBGRA(color.R, color.G, color.B, color.A);
        }
    }
}