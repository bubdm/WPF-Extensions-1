using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF_Extension.ColorExtensions
{
    public struct HsvColor
    {

        public double H { get; set; }

        public double S { get; set; }

        public double V { get; set; }

        public HsvColor(double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }

        public Color ToRGB()
        {
            var r = 0.0d;
            var g = 0.0d;
            var b = 0.0d;

            if (S == 0)
            {
                r = V;
                g = V;
                b = V;
            }
            else
            {
                int i;
                double f, p, q, t;

                if (H == 360)
                    H = 0;
                else
                    H = H / 60;

                i = (int)Math.Truncate(H);
                f = H - i;

                p = V * (1.0 - S);
                q = V * (1.0 - (S * f));
                t = V * (1.0 - (S * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        {
                            r = V;
                            g = t;
                            b = p;
                            break;
                        }
                    case 1:
                        {
                            r = q;
                            g = V;
                            b = p;
                            break;
                        }
                    case 2:
                        {
                            r = p;
                            g = V;
                            b = t;
                            break;
                        }
                    case 3:
                        {
                            r = p;
                            g = q;
                            b = V;
                            break;
                        }
                    case 4:
                        {
                            r = t;
                            g = p;
                            b = V;
                            break;
                        }
                    default:
                        {
                            r = V;
                            g = p;
                            b = q;
                            break;
                        }
                }

            }

            return Color.FromArgb(255, (byte)Math.Round(r * 255), (byte)Math.Round(g * 255), (byte)Math.Round(b * 255));

        }


        public static Color ToRGB(double h, double s, double v)
        {
            double r = 0, g = 0, b = 0;

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                int i;
                double f, p, q, t;

                h = h == 360 ? 0 : h / 60;

                i = (int)Math.Truncate(h);
                f = h - i;

                p = v * (1.0 - s);
                q = v * (1.0 - s * f);
                t = v * (1.0 - s * (1.0 - f));

                switch (i)
                {
                    case 0:
                        {
                            r = v;
                            g = t;
                            b = p;
                            break;
                        }
                    case 1:
                        {
                            r = q;
                            g = v;
                            b = p;
                            break;
                        }
                    case 2:
                        {
                            r = p;
                            g = v;
                            b = t;
                            break;
                        }
                    case 3:
                        {
                            r = p;
                            g = q;
                            b = v;
                            break;
                        }
                    case 4:
                        {
                            r = t;
                            g = p;
                            b = v;
                            break;
                        }
                    default:
                        {
                            r = v;
                            g = p;
                            b = q;
                            break;
                        }
                }

            }

            return Color.FromArgb(255, (byte)Math.Round(r * 255), (byte)Math.Round(g * 255), (byte)Math.Round(b * 255));
        }

    }
}
