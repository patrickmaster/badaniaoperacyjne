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
            Solution = new List<int>();
            Iterations = new List<IterationBlock>();
        }

        /// <summary>
        /// Koszt ostateczny przejazdu po wyznaczonej trasie
        /// </summary>
        public double TotalCost { get; set; }

        /// <summary>
        /// Kolejność przejazdu komiwojażerka
        /// </summary>
        public List<int> Solution { get; set; }

        /// <summary>
        /// Lista iteracji.
        /// </summary>
        public List<IterationBlock> Iterations { get; set; }

        /// <summary>
        /// Czas trwania rozwiązywania
        /// </summary>
        public double SolvingTime { get; set; }

        /// <summary>
        /// Status zakończenia algorytmu
        /// </summary>
        public OutputState State { get; set; }
    }

    public enum OutputState
    {
        Done,
        Cancelled,
        NoSolution
    }
}
