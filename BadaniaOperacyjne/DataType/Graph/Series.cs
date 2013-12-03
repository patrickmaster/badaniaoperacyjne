using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.Graph
{
    public class Series
    {
        public Series()
        {
            Values = new List<Point>();
        }

        public string Description { get; set; }

        public List<Point> Values { get; set; }
    }
}
