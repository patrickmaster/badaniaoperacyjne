using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType;

namespace BadaniaOperacyjne.Solver
{
    public interface ISolver
    {
        OutputData Solve(InputData input, Settings settings);
    }
}
