using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BadaniaOperacyjne.Controls.Problem
{
    public static class Translator
    {
        internal static double GetScale(double scale)
        {
            return Math.Exp(scale / 10d);
        }

        internal static Point ScalePoint(Point point, double scale)
        {
            return new Point(
                point.X * GetScale(scale),
                point.Y * GetScale(scale));
        }

        internal static Point DescalePoint(Point point, double scale)
        {
            return new Point(
                point.X / GetScale(scale),
                point.Y / GetScale(scale));
        }
        
        internal static Point GetRelativeCoords(Point point, Size size, Point position)
        {
            return new Point(
                //point.X + size.Width / 2d + Position.X + draggingDelta.X, 
                //point.Y + size.Height / 2d + Position.Y + draggingDelta.Y);
                point.X + size.Width / 2d + position.X,
                point.Y + size.Height / 2d + position.Y);
        }

        internal static Point GetAbsoluteCoords(Point point, Size size, Point position)
        {
            //absolute + size.Width / 2d + Position.X + draggingDelta.X = relative
            return new Point(
                point.X - size.Width / 2d - position.X,
                point.Y - size.Height / 2d - position.Y);
        }
    }
}
