using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BadaniaOperacyjne.Controls.Problem
{
    public class Place
    {
        public Point Position { get; set; }
        public PlaceType Type { get; set; }
    }

    public enum PlaceType
    {
        Package,
        Petrol
    }
}
