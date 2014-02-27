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

        delegate void Operation(List<int> oldSolution, List<int> newSolution);

        public OutputData Solve(InputData input, Settings settings, BackgroundWorker worker)
        {
            // TODO
            // Gdy problem zadany bez stacji (fuel cap = 0), to coś sie psuje
            OutputData result = new OutputData();

            Operation operation = null;

            switch (settings.Operation)
            {
                case OperationType.Operation1:
                    operation = Operation1;
                    break;
                case OperationType.Operation2:
                    operation = Operation2;
                    break;
            }

            List<double> iterationBlockCosts = new List<double>(); 

            double currentCost = 0;
            double newCost = 0;
            double subtract = 0;

            int numIterations = settings.NumIterations;
            double temperature = settings.StartingTemperature;

            List<int> currentSolution = null;
            
            List<int> currentSolutionWithPetrolPlaces = new List<int>();
            if (input.FuelCapacity > 0)
            {
                int counter = 0;
                do
                {
                    if (counter > Math.Pow(input.NumPlaces, 2))
                    {
                        result.State = OutputState.NoSolution;
                        return result;
                    }
                    currentSolution = GetStartingSolution(input);
                    currentSolution.CopyListTo(currentSolutionWithPetrolPlaces);
                    counter++;
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
                double 
                    minCost = currentCost, 
                    maxCost = currentCost;

                for (int i = 0; i < numIterations; i++)
                {
                    if (input.FuelCapacity > 0)
                    {
                        do
                        {
                            operation(currentSolution, newSolution);
                            newSolution.CopyListTo(newSolutionWithPetrolPlaces);
                        }
                        while (!PutPetrolPlaces(input, newSolutionWithPetrolPlaces));
                    }
                    else
                    {
                        operation(currentSolution, newSolution);
                        newSolution.CopyListTo(newSolutionWithPetrolPlaces);
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

                        if (currentCost > maxCost)
                            maxCost = currentCost;
                        else if (currentCost < minCost)
                            minCost = currentCost;

                        newSolution.CopyListTo(currentSolution);
                        newSolutionWithPetrolPlaces.CopyListTo(currentSolutionWithPetrolPlaces);

                        if (subtract <= 0)
                            block.ProgressionCount++;
                        else
                            block.RegressionCount++;
                    }

                    //block.Values.Add(currentCost);
                    iterationBlockCosts.Add(currentCost);
                    if (iterationBlockCosts.Count == (int)(numIterations / settings.PointsPerIterationBlock))
                    {
                        block.Values.Add(iterationBlockCosts.ReduceCollectionToValue(x => x.Average()));
                        iterationBlockCosts.Clear();
                    }

                    if (worker.CancellationPending == true)
                    {
                        result.State = OutputState.Cancelled;
                        return result;
                    }
                }

                temperature *= settings.CoolingCoefficient;
                block.CurrentTemperature = temperature;
                numIterations = (int)((double)numIterations * settings.NumIterationsMultiplier);
                //block.Values = iterationBlockCosts.ReduceCollection(
                //    settings.PointsPerIterationBlock,
                //    x => x.Average()).ToList();
                block.Minimum = minCost;
                block.Maximum = maxCost;

                iterationBlockCosts.Clear();

                worker.ReportProgress(0, block);
                result.Iterations.Add(block);
            }

            result.Solution = currentSolutionWithPetrolPlaces;
            result.TotalCost = currentCost;

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
            List<int> packagePlaces = new List<int>();

            //solution[0] = 0;
            for (int i = 1, j = 1; i < problem.NumPlaces && j < count - 1; i++)
            {
                if (problem.PetrolPlaces.IndexOf(i) == -1)
                {
                    //solution[j] = i;
                    j++;
                    packagePlaces.Add(i);
                }
            }

            //solution[count - 1] = 0;
            int packagePlacesCount = packagePlaces.Count;
            for (int i = 0; i < packagePlacesCount; i++)
            {
                int index = rand.Next(packagePlaces.Count - 1);
                solution[i + 1] = packagePlaces[index];
                packagePlaces.RemoveAt(index);
            }

            return solution;
        }

        static void Operation1(List<int> currentSolution, List<int> newSolution)
        {
            //newSolution.CopyTo(currentSolution, 0);
            //newSolution.CopyListTo(currentSolution);
            currentSolution.CopyListTo(newSolution);
            int pos1 = rand.Next(1, newSolution.Count - 1);
            int pos2 = rand.Next(1, newSolution.Count - 1);
            int temp = newSolution[pos1];
            newSolution[pos1] = newSolution[pos2];
            newSolution[pos2] = temp;
        }

        static void Operation2(List<int> currentSolution, List<int> newSolution)
        {
            currentSolution.CopyListTo(newSolution);
            // length <nalezy do> [1, count - 1]
            // uwaga: next losuje z wyłączeniem max wartosci
            // = 9
            int count = newSolution.Count - 2;
            // [1, 8]
            int length = rand.Next(count - 1) + 1;
            // [0, 1] 
            int start = rand.Next(count - length + 1);
            // [-1, 9 - 1 - 8]
            int slide = rand.Next(-start, count - start - length + 1);
            newSolution.MoveBlock(start + 1, slide, length);
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
                    if (input.PetrolPlaces.IndexOf(solution[closestPetrol.IndexOfLastPlaceBeforeOutOfFuel]) == -1)
                        solution.Insert(closestPetrol.IndexOfLastPlaceBeforeOutOfFuel + 1, closestPetrolPlace);
                    else
                        return false;
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
