using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType
{
    public class Iteration
    {
        public Iteration()
        {
            IterationNumber = 0;
        }
        public Iteration(int num)
        {
            IterationNumber = num;
        }

        /// <summary>
        /// Wartość funkcji celu w danej iteracji
        /// </summary>
        public double Cost { get; set; }

        public int IterationNumber { get; private set; }
    }
}
