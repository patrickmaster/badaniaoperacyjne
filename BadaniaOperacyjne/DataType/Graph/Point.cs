using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.Graph
{
    public class Point
    {
        public Point(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        
        public double Y { get; set; }

        public string Description { get; set; }
    }
}
