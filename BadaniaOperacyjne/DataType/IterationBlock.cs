using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType
{
    /// <summary>
    /// Klasa zawierająca wszelkie istotne dane dotyczące każdej iteracji
    /// </summary>
    public class IterationBlock
    {
        public IterationBlock()
        {
            Values = new List<double>();
        }

        /// <summary>
        /// Ilość popraw w tym bloku iteracji
        /// </summary>
        public int ProgressionCount { get; set; }

        /// <summary>
        /// Ilość pogorszeń w tym bloku iteracji
        /// </summary>
        public int RegressionCount { get; set; }

        /// <summary>
        /// Uśredniona lista wartości w tym bloku
        /// </summary>
        public List<double> Values { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        /// <summary>
        /// Temperatura średnia dla bloku iteracji
        /// </summary>
        public double CurrentTemperature { get; set; }
    }
}
