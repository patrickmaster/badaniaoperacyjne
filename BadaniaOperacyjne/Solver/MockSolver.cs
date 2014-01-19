using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType;
using System.ComponentModel;

namespace BadaniaOperacyjne.Solver
{
    public class MockSolver : ISolver
    {
        public OutputData Solve(InputData input, Settings settings, BackgroundWorker worker)
        {
            Random rand = new Random();
            OutputData result = new OutputData();

            for (int i = 0; i < 200; i++)
            {
                IterationBlock block = new IterationBlock
                {
                    ProgressionCount = rand.Next(10),
                    RegressionCount = rand.Next(10)
                };

                for (int j = 0; j < 10; j++)
                {
                    block.Iterations.Add(new Iteration(j)
                    {
                        Cost = Math.Sin(i * (double)j * 10)
                    });
                }

                worker.ReportProgress(0, block);
                result.Iterations.Add(block);

                if (worker.CancellationPending == true)
                {
                    return null;
                }

                System.Threading.Thread.Sleep(50);
            }


            result.TotalCost = 10;
            result.Order = new List<int> { 4, 5, 6, 3, 2, 1, 0, 6, 5, 4, 3, 3, 4, 234, 234, 32, 34, 4, 43, 4 };

            return result;
        }
    }
}
