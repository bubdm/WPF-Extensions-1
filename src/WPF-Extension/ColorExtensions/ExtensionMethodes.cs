using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF_Extension.ColorExtensions
{
    public static class ExtensionMethodes
    {
        public static HsvColor ToHSVColor(this Color color)
        {
            var r = color.R;
            var g = color.G;
            var b = color.B;
            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(r, g), b);
            v = Math.Max(Math.Max(r, g), b);
            delta = v - min;

            s = v == 0 ? 0 : delta / v;

            if (s == 0)
                h = 0;

            else
            {
                if (r == v)
                    h = (g - b) / delta;
                else if (g == v)
                    h = 2 + (b - r) / delta;
                else if (b == v)
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h < 0)
                    h += 360;

            }

            return new HsvColor(h, s, v / 255);
        }

        public static double GetHue(this Color color) 
            => color.ToHSVColor().H;

        public static Color Blend(this Color color, Color backColor)
        {
            var opacity = backColor.A / 255.0;
            byte r = (byte)(backColor.R * opacity + color.R * (1 - opacity));
            byte g = (byte)(backColor.G * opacity + color.G * (1 - opacity));
            byte b = (byte)(backColor.B * opacity + color.B * (1 - opacity));
            return Color.FromArgb(255,r, g, b);
        }

        public static Color GetColorAtOffset(this LinearGradientBrush brush, double offset)
        {
            var gsc = brush.GradientStops;
            var point = gsc.SingleOrDefault(f => f.Offset == offset);
            if (point != null) return point.Color;

            GradientStop before = gsc.Where(w => w.Offset == gsc.Min(m => m.Offset)).First();
            GradientStop after = gsc.Where(w => w.Offset == gsc.Max(m => m.Offset)).First();

            foreach (var gs in gsc)
            {
                if (gs.Offset < offset && gs.Offset > before.Offset)
                {
                    before = gs;
                }
                if (gs.Offset > offset && gs.Offset < after.Offset)
                {
                    after = gs;
                }
            }

            var color = new Color
            {
                A = (byte)((offset - before.Offset) * (after.Color.A - before.Color.A) / (after.Offset - before.Offset) + before.Color.A),
                R = (byte)((offset - before.Offset) * (after.Color.R - before.Color.R) / (after.Offset - before.Offset) + before.Color.R),
                G = (byte)((offset - before.Offset) * (after.Color.G - before.Color.G) / (after.Offset - before.Offset) + before.Color.G),
                B = (byte)((offset - before.Offset) * (after.Color.B - before.Color.B) / (after.Offset - before.Offset) + before.Color.B)
            };

            return color;
        }

        public static double GetOffsetByColor(this LinearGradientBrush brush, Color color)
        {
            var gsc = brush.GradientStops;
            var p = gsc.SingleOrDefault(gs => gs.Color == color);

            GradientStop before = gsc.Where(w => w.Color.GetHue() == gsc.Min(m => m.Color.GetHue())).First();
            GradientStop after = gsc.Where(w => w.Color.GetHue() == gsc.Max(m => m.Color.GetHue())).First();

            foreach (var gs in gsc)
            {
                if (gs.Color.GetHue() < color.GetHue() && gs.Color.GetHue() > before.Color.GetHue())
                    before = gs;
                if (gs.Color.GetHue() > color.GetHue() && gs.Color.GetHue() < before.Color.GetHue())
                    after = gs;
            }

            return after.Color.GetHue() - before.Color.GetHue();

        }

        public static int GetDiff(this Color col1, Color col2) 
            => (int)Math.Sqrt((col1.R - col2.R) * (col1.R - col2.R)
                               + (col1.G - col2.G) * (col1.G - col2.G)
                               + (col1.B - col2.B) * (col1.B - col2.B));

    }
}
