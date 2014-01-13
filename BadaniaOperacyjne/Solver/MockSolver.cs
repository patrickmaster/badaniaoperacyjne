using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadaniaOperacyjne.DataType;
using System.ComponentModel;

namespace BadaniaOperacyjne.Solver
{
    public class MockSolver : ISolver
    {
        /*
        public OutputData SolveLol(InputData input, Settings settings)
        {
            OutputData result = new OutputData();

            for (int i = 0; i < 100; i++)
            {
                result.Iterations.Add(new IterationBlock { Cost = Math.Sin(i / 10.0) });
            }

            return result;
        }
        */

        private InputData input;
        private Settings settings;
        private IterationBlock iterationBlock;
        private BackgroundWorker worker;

        public event SolverProgressEventHandler SolverProgress;
        public event SolverDoneEventHandler SolverDone;
        public event SolverCancelEventHandler SolverCancel;

        public bool IsBusy
        {
            get
            {
                if (worker != null)
                {
                    return worker.IsBusy;
                }
                else
                    return false;
            }
        }

        public MockSolver()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            e.Result = Solve(input, settings, worker, e);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SolverProgress(this, new SolverProgressEventArgs
            {
                Block = iterationBlock
            });
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OutputData output = e.Result as OutputData;

            SolverDone(this, new SolverDoneEventArgs
            {
                Output = output
            });
        }

        private OutputData Solve(InputData input, Settings settings, BackgroundWorker worker, DoWorkEventArgs e)
        {
            //System.Console.Write(input.NumPlaces);
            //System.Console.Write("\n");
            /*
            double CurrentCost = 0;
            double NewCost = 0;
            double Subtract = 0;
            Random rnd = new Random();

            //double[,] Matrix = new double[,]{{0, 5, 10, 2, 7}, {5, 0, 15, 3, 9}, {1, 11, 0, 5, 8}, {8, 4, 5, 0, 9}, {1, 6, 3, 9, 0}};

            int[] CurrentSolution = new int[input.NumPlaces + 1];
            int[] NewSolution = new int[input.NumPlaces + 1];

            for (int i = 0; i < input.NumPlaces; i++)
            {
                CurrentSolution[i] = i;
                NewSolution[i] = i;
            }
            CurrentSolution[input.NumPlaces] = 0;
            NewSolution[input.NumPlaces] = 0;


            for (int i = 0; i < input.NumPlaces + 1; i++)
            {
                System.Console.Write(CurrentSolution[i]);
                System.Console.Write("\n");
            }
            //System.Console.Write("test                      test\n");

            while (settings.StartingTemperature > settings.EndingTemperature)
            {
                IterationBlock block = new IterationBlock();

                System.Console.Write("Liczba iteracji:      ");
                System.Console.Write(settings.NumIterations);
                System.Console.Write("\n");
                System.Console.Write("Temperatura:      ");
                System.Console.Write(settings.StartingTemperature);
                System.Console.Write("\n");

                for (int i = 0; i < settings.NumIterations; i++)
                {
                    Iteration iteration = new Iteration(i);

                    System.Console.Write("iteracja      ");
                    System.Console.Write(i);
                    System.Console.Write("\n");

                    for (int k = 0; k < input.NumPlaces; k++)
                    {
                        NewSolution[k] = CurrentSolution[k];
                    }
                    int Change1 = rnd.Next(1, input.NumPlaces - 1);       //numer miasta do zamiany miejscami
                    int Change2 = rnd.Next(1, input.NumPlaces - 1);       //pierwsza metoda mieszania
                    int City = NewSolution[Change1];
                    NewSolution[Change1] = NewSolution[Change2];
                    NewSolution[Change2] = City;

                    NewCost = 0;

                    for (int a = 0; a < input.NumPlaces; a++)
                    {
                        NewCost = NewCost + input.Places[NewSolution[a]][NewSolution[a + 1]];
                    }

                    for (int j = 0; j < input.NumPlaces + 1; j++)
                    {
                        System.Console.Write(NewSolution[j]);
                        System.Console.Write("\n");
                    }

                    CurrentCost = 0;

                    for (int n = 0; n < input.NumPlaces; n++)
                    {
                        double cost = input.Places[CurrentSolution[n]][CurrentSolution[n + 1]];
                        CurrentCost = CurrentCost + cost;
                    }

                    System.Console.Write("CurrentCost              ");
                    System.Console.Write(CurrentCost);
                    System.Console.Write("\n");

                    Subtract = NewCost - CurrentCost;
                    double randomNumber = rnd.NextDouble();
                    double temp = Math.Exp((-1) * (Subtract / settings.StartingTemperature));
                    if (Subtract < 0 || (Subtract > 0) && (temp > randomNumber))
                    {
                        CurrentCost = NewCost;
                        CurrentSolution = NewSolution;
                    }

                    iteration.Cost = CurrentCost;

                    block.Iterations.Add(iteration);
                }

                settings.StartingTemperature = settings.StartingTemperature * settings.CoolingCoefficient;
                settings.NumIterations = (int)((double)settings.NumIterations * settings.NumIterationsMultiplier);

                System.Console.Write("\n");

                iterationBlock = block;
                worker.ReportProgress(0);
            }
            */

            for (int i = 0; i < 100; i++)
            {
                IterationBlock block = new IterationBlock
                {
                    ProgressionCount = i % 5,
                    RegressionCount = i % 7
                };

                for (int j = 0; j < 10; j++)
                {
                    block.Iterations.Add(new Iteration(j)
                    {
                        Cost = Math.Sin(i * (double)j / 10)
                    });
                }

                //SolverProgress(this, new SolverProgressEventArgs
                //{
                //    Block = block
                //});
                iterationBlock = block;
                worker.ReportProgress(0);

                System.Threading.Thread.Sleep(100);
            }

            OutputData result = new OutputData();

            result.TotalCost = 10;
            result.Order = new List<uint> { 4, 5, 6, 3, 2, 1, 0 };

            return result;
        }

        public void SolveAsync(InputData input, Settings settings)
        {
            if (worker.IsBusy == true)
            {
                throw new SolverBusyException();
            }

            this.input = input;
            this.settings = settings;

            worker.RunWorkerAsync();
        }

        public void CancelSolve()
        {
            if (worker.IsBusy == true)
            {
                try
                {
                    worker.CancelAsync();
                    SolverCancel(this, new SolverCancelEventArgs
                    {
                    });
                }
                catch { }
            }
        }
    }
}
