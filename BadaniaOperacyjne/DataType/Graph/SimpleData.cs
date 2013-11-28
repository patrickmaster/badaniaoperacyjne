using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.Graph
{
    public class SimpleData
    {
        public SimpleData()
        {
            HorizontalValues = new List<double>();
            VerticalValues = new ValueLine();
        }

        /// <summary>
        /// Opis dla osi poziomej wykresu
        /// </summary>
        public string HorizontalAxisTitle { get; set; }

        /// <summary>
        /// Opis dla osi pionowej wykresu
        /// </summary>
        public string VerticalAxisTitle { get; set; }

        /// <summary>
        /// Tytuł wykresu
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Wartości osi poziomej
        /// </summary>
        public List<double> HorizontalValues { get; set; }

        /// <summary>
        /// Wartości osi pionowej
        /// </summary>
        public ValueLine VerticalValues { get; set; }
    }
}
