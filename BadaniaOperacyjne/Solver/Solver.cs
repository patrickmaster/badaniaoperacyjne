using BadaniaOperacyjne.DataType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.Solver
{
    class Solver : ISolver
    {
        public OutputData Solve(InputData input, Settings settings, BackgroundWorker worker)
        {
            OutputData result = new OutputData();

            double CurrentCost = 0;
            double NewCost = 0;
            double Subtract = 0;
            Random rnd = new Random();

            int[] CurrentSolution = new int[input.NumPlaces + 1];
            int[] NewSolution = new int[input.NumPlaces + 1];

            for (int i = 0; i < input.NumPlaces; i++)
            {
                CurrentSolution[i] = i;
                NewSolution[i] = i;
            }
            CurrentSolution[input.NumPlaces] = 0;
            NewSolution[input.NumPlaces] = 0;

            for (int n = 0; n < input.NumPlaces; n++)
            {
                CurrentCost = CurrentCost + input.Places[CurrentSolution[n]][CurrentSolution[n + 1]];
            }

            while (settings.StartingTemperature > settings.EndingTemperature)
            {
                IterationBlock block = new IterationBlock();

                for (int i = 0; i < settings.NumIterations; i++)
                {
                    Iteration iteration = new Iteration(i);

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

                    Subtract = NewCost - CurrentCost;
                    double randomik = rnd.NextDouble();
                    double zmienna = Math.Exp((-1) * (Subtract / settings.StartingTemperature));
                    if (Subtract <= 0 || (Subtract > 0) && (zmienna > randomik))
                    {
                        CurrentCost = NewCost;
                        //CurrentSolution = NewSolution;
                        for (int k = 0; k < input.NumPlaces; k++)
                        {
                            CurrentSolution[k] = NewSolution[k];
                        }
                    }

                    iteration.Cost = NewCost;

                    block.Iterations.Add(iteration);

                    if (worker.CancellationPending == true)
                        return null;
                }

                settings.StartingTemperature = settings.StartingTemperature * settings.CoolingCoefficient;
                block.CurrentTemperature = settings.StartingTemperature;
                settings.NumIterations = (int)((double)settings.NumIterations * settings.NumIterationsMultiplier);

                worker.ReportProgress(0, block);
                result.Iterations.Add(block);
            }

            return result;
        }
    }
}
