using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType;

namespace BadaniaOperacyjne.Solver
{
    public class SolverDoneEventArgs : EventArgs
    {
        public OutputData Output { get; set; }
    }
}
