using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BadaniaOperacyjne.Extensions
{
    public static class Point
    {
        public static double GetDistance(this System.Windows.Point point1, System.Windows.Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
    }
}
