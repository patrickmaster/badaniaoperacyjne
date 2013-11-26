using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.Graph
{
    public class ValueLine
    {
        public ValueLine()
        {
            Values = new List<double>();
            Title = "Data";
            Brush = System.Windows.Media.Brushes.Blue;
            Size = 4;
        }

        /// <summary>
        /// Lista wartości do umieszczenia na osi Y
        /// </summary>
        public IEnumerable<double> Values { get; set; }

        /// <summary>
        /// Opis wykresu
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Kolor pyndzla
        /// </summary>
        public System.Windows.Media.Brush Brush { get; set; }

        /// <summary>
        /// Rozmiar punkciku
        /// </summary>
        public double Size { get; set; }
    }
}
