using BadaniaOperacyjne.DataType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.Extensions;

namespace BadaniaOperacyjne.Solver
{
    public class Solver : ISolver
    {
        private static Random rand = new Random();

        public class ClosestPetrol
        {
            public int LastPlaceBeforeOutOfFuel { get; set; }
            public int IndexOfLastPlaceBeforeOutOfFuel { get; set; }
            public double FuelLeft { get; set; }
        }

        public OutputData Solve(InputData input, Settings settings, BackgroundWorker worker)
        {
            // TODO
            // Gdy problem zadany bez stacji (fuel cap = 0), to coś sie psuje
            OutputData result = new OutputData();

            double currentCost = 0;
            double newCost = 0;
            double subtract = 0;

            int numIterations = settings.NumIterations;
            double temperature = settings.StartingTemperature;

            List<int> currentSolution = null;
            
            List<int> currentSolutionWithPetrolPlaces = new List<int>();
            if (input.FuelCapacity > 0)
            {
                do
                {
                    currentSolution = GetStartingSolution(input);
                    currentSolution.CopyListTo(currentSolutionWithPetrolPlaces);
                }
                while (!PutPetrolPlaces(input, currentSolutionWithPetrolPlaces));
            }
            else
            {
                currentSolution = GetStartingSolution(input);
                currentSolution.CopyListTo(currentSolutionWithPetrolPlaces);
            }

            List<int> newSolution = new List<int>(new int[currentSolution.Count]);
            List<int> newSolutionWithPetrolPlaces = new List<int>();
            currentSolution.CopyListTo(newSolution);

            int currentSolutionCountMinusOne = currentSolutionWithPetrolPlaces.Count() - 1;
            for (int i = 0; i < currentSolutionCountMinusOne; i++)
            {
                //CurrentCost = CurrentCost + input.Places[CurrentSolution[i]][CurrentSolution[i + 1]];
                currentCost += input.Places[currentSolutionWithPetrolPlaces[i]][currentSolutionWithPetrolPlaces[i + 1]];
            }

            while (temperature > settings.EndingTemperature)
            {
                IterationBlock block = new IterationBlock();

                for (int i = 0; i < numIterations; i++)
                {
                    Iteration iteration = new Iteration(i);

                    if (input.FuelCapacity > 0)
                    {
                        do
                        {
                            Operation1(currentSolution, newSolution);
                            newSolution.CopyListTo(newSolutionWithPetrolPlaces);
                        }
                        while (!PutPetrolPlaces(input, newSolutionWithPetrolPlaces));
                    }
                    else
                    {
                        Operation1(currentSolution, newSolution);
                    }

                    newCost = 0;

                    int newSolutionCountMinusOne = newSolutionWithPetrolPlaces.Count() - 1;
                    for (int j = 0; j < newSolutionCountMinusOne; j++)
                    {
                        newCost += input.Places[newSolutionWithPetrolPlaces[j]][newSolutionWithPetrolPlaces[j + 1]];
                    }

                    subtract = newCost - currentCost;
                    double randomik = rand.NextDouble();
                    double zmienna = Math.Exp((-1) * (subtract / temperature));
                    if (subtract <= 0 || subtract > 0 && zmienna > randomik)
                    {
                        currentCost = newCost;

                        newSolution.CopyListTo(currentSolution);

                        if (subtract <= 0)
                            block.ProgressionCount++;
                        else
                            block.RegressionCount++;
                    }

                    iteration.Cost = currentCost;

                    block.Iterations.Add(iteration);

                    if (worker.CancellationPending == true)
                    {
                        result.State = OutputState.Cancelled;
                        return result;
                    }
                }

                temperature *= settings.CoolingCoefficient;
                block.CurrentTemperature = temperature;
                numIterations = (int)((double)numIterations * settings.NumIterationsMultiplier);

                worker.ReportProgress(0, block);
                result.Iterations.Add(block);
            }

            result.Solution = currentSolutionWithPetrolPlaces;
            result.TotalCost = newCost;

            result.State = OutputState.Done;
            return result;
        }

        public static List<int> GetStartingSolution(InputData problem)
        {
            int count;
            if (problem.FuelCapacity == 0)
            {
                problem.PetrolPlaces.Clear();
            }
            count = problem.NumPlaces - problem.PetrolPlaces.Count + 1;
            List<int> solution = new List<int>(new int[count]);

            //solution[0] = 0;
            for (int i = 1, j = 1; i < problem.NumPlaces && j < count - 1; i++)
            {
                if (problem.PetrolPlaces.IndexOf(i) == -1)
                {
                    solution[j] = i;
                    j++;
                }
            }
            solution[count - 1] = 0;

            return solution;
        }

        public static void Operation1(List<int> currentSolution, List<int> newSolution)
        {
            //newSolution.CopyTo(currentSolution, 0);
            newSolution.CopyListTo(currentSolution);
            int pos1 = rand.Next(1, newSolution.Count - 1);
            int pos2 = rand.Next(1, newSolution.Count - 1);
            int temp = newSolution[pos1];
            newSolution[pos1] = newSolution[pos2];
            newSolution[pos2] = temp;
        }

        public static ClosestPetrol CheckIfPossible(InputData problem, List<int> solution)
        {
            ClosestPetrol closestPetrol = new ClosestPetrol();
            double fuel = problem.FuelCapacity;
            double solutionLength = solution.Count;
            for (int i = 0; i < solutionLength - 1; i++)
            {
                //if (Array.IndexOf<int>(problem.PetrolPlaces.ToArray(), solution[i]) != -1)
                if (problem.PetrolPlaces.IndexOf(solution[i]) != -1)
                    fuel = problem.FuelCapacity;
                fuel -= problem.Places[solution[i]][solution[i + 1]];
                if (fuel < 0)
                {
                    closestPetrol.FuelLeft = fuel + problem.Places[solution[i]][solution[i + 1]];
                    closestPetrol.IndexOfLastPlaceBeforeOutOfFuel = i;
                    closestPetrol.LastPlaceBeforeOutOfFuel = solution[i];
                    return closestPetrol;
                }
            }
            return null;
        }

        /// <summary>
        /// Poszukuje najbliższej stacji od danego miasta
        /// </summary>
        /// <param name="problem"></param>
        /// <param name="place">Miasto, z którego poszukiwana jest stacja</param>
        /// <param name="distanceLimit">Limit odległości</param>
        /// <returns>Zwraca numer miasta-stacji</returns>
        public static int FindClosestPetrolPlace(InputData problem, int place, double distanceLimit)
        {
            double minDistance = 0;
            int closestPlace = -1;
            foreach (int petrolPlace in problem.PetrolPlaces)
            {
                if (minDistance == 0 || problem.Places[place][petrolPlace] < minDistance)
                {
                    minDistance = problem.Places[place][petrolPlace];
                    closestPlace = petrolPlace;
                }
            }

            if (closestPlace != -1 && problem.Places[place][closestPlace] < distanceLimit)
                return closestPlace;
            else return -1;
        }

        public static bool PutPetrolPlaces(InputData input, List<int> solution)
        {
            ClosestPetrol closestPetrol = null;
            int closestPetrolPlace = -1;

            while ((closestPetrol = CheckIfPossible(input, solution)) != null)
            {
                while ((closestPetrolPlace = FindClosestPetrolPlace(input, solution[closestPetrol.IndexOfLastPlaceBeforeOutOfFuel], closestPetrol.FuelLeft)) == -1
                    && closestPetrol.IndexOfLastPlaceBeforeOutOfFuel > 0)
                {
                    // Cofnij się
                    closestPetrol.IndexOfLastPlaceBeforeOutOfFuel--;
                    //closestPetrol.FuelLeft += input.Places[closestPetrol.IndexOfLastPlaceBeforeOutOfFuel][closestPetrol.IndexOfLastPlaceBeforeOutOfFuel + 1];
                    closestPetrol.FuelLeft += input.Places[solution[closestPetrol.IndexOfLastPlaceBeforeOutOfFuel]][solution[closestPetrol.IndexOfLastPlaceBeforeOutOfFuel + 1]];
                }
                if (closestPetrol.IndexOfLastPlaceBeforeOutOfFuel != 0)
                {
                    solution.Insert(closestPetrol.IndexOfLastPlaceBeforeOutOfFuel + 1, closestPetrolPlace);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
