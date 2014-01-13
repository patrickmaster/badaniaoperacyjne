using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType
{
    public class OutputData
    {
        public OutputData()
        {
            Order = new List<uint>();
            Iterations = new List<IterationBlock>();
        }

        /// <summary>
        /// Kolejność przejazdu komiwojażerka
        /// </summary>
        public List<uint> Order { get; set; }

        /// <summary>
        /// Lista iteracji.
        /// </summary>
        public List<IterationBlock> Iterations { get; set; }

        /// <summary>
        /// Koszt ostateczny przejazdu po wyznaczonej trasie
        /// </summary>
        public double TotalCost { get; set; }
    }
}
