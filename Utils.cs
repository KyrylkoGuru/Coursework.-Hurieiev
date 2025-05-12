using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAurora
{
    public static class Utils
    {
        public static bool IsNear(Point a, Point b, int threshold = 5)
        {
            return Math.Abs(a.X - b.X) < threshold && Math.Abs(a.Y - b.Y) < threshold;
        }
    }
}