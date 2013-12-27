using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType
{
    /// <summary>
    /// Klasa zawierająca wszelkie istotne dane dotyczące każdej iteracji
    /// </summary>
    public class  Iteration
    {
        protected static int iterationNumber = 0;

        public Iteration()
        {
            IterationNumber = ++iterationNumber;
        }
        /// <summary>
        /// Numer kolejnej iteracji
        /// </summary>
        public int IterationNumber { get; private set; }
        /// <summary>
        /// Wartość funkcji celu w danej iteracji
        /// </summary>
        public double Cost { get; set; }
    }
}
