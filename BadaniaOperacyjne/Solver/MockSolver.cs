using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType;

namespace BadaniaOperacyjne.Solver
{
    public class MockSolver : ISolver
    {
        public OutputData Solve(InputData input, Settings settings)
        {
            OutputData result = new OutputData();

            for (int i = 0; i < 100; i++)
            {
                result.Iterations.Add(new Iteration { Cost = Math.Sin(i / 10.0) });
            }

            return result;
        }
    }
}
