using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType;

namespace BadaniaOperacyjne.Solver
{
    public class SolverProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Właśnie zakończony blok iteracyjny
        /// </summary>
        public IterationBlock Block { get; set; }
    }
}
