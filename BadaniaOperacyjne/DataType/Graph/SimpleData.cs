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
            Series = new Series();
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
        /// Wartości osi pionowej
        /// </summary>
        public Series Series { get; set; }
    }
}
