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
            Iterations = new List<Iteration>();
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
        /// Lista iteracji w tym bloku
        /// </summary>
        public List<Iteration> Iterations { get; set; }

        /// <summary>
        /// Temperatura średnia dla bloku iteracji
        /// </summary>
        public double CurrentTemperature { get; set; }
    }
}
