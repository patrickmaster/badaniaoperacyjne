using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType.DataFlow;

namespace BadaniaOperacyjne.Solver
{
    public interface ISolver
    {
        OutputData Solve(InputData input);
    }
}
