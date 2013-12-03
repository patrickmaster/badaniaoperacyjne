using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.Graph
{
    /// <summary>
    /// Klasa służąca do reprezentowania danych dla wykresu o niezależnych
    /// argumentach dla każdego zestawu wartości
    /// </summary>
    public class IndependentArguments : SimpleData
    {
        public IndependentArguments()
            : base()
        {
            Series = new List<Series>();
        }

        public new List<Series> Series { get; set; }
    }
}
