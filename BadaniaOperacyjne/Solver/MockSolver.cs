using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType.DataFlow;

namespace BadaniaOperacyjne.Solver
{
    public class MockSolver : ISolver
    {
        public OutputData Solve(InputData input)
        {
            OutputData result = new OutputData();

            return result;
        }
    }
}
