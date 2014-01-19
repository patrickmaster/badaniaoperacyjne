using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.Solver
{
    public class SolverBeginEventArgs : EventArgs
    {
        public double StartingTemperature { get; set; }
        public double EndingTemperature { get; set; }
    }
}
