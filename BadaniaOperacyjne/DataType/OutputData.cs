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
            Order = new List<int>();
            Iterations = new List<IterationBlock>();
        }

        /// <summary>
        /// Koszt ostateczny przejazdu po wyznaczonej trasie
        /// </summary>
        public double TotalCost { get; set; }

        /// <summary>
        /// Kolejność przejazdu komiwojażerka
        /// </summary>
        public List<int> Order { get; set; }

        /// <summary>
        /// Lista iteracji.
        /// </summary>
        public List<IterationBlock> Iterations { get; set; }
    }
}
