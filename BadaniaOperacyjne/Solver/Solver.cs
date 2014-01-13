using BadaniaOperacyjne.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.Solver
{
    class Solver// : ISolver
    {
        public OutputData Solve(InputData input, Settings settings)
        {
            //System.Console.Write(input.NumPlaces);
            //System.Console.Write("\n");
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
                System.Console.Write("Liczba iteracji:      ");
                System.Console.Write(settings.NumIterations);
                System.Console.Write("\n");
                System.Console.Write("Temperatura:      ");
                System.Console.Write(settings.StartingTemperature);
                System.Console.Write("\n");

                //System.Console.Write("test\n");

                for (int i = 0; i < settings.NumIterations; i++)
                {
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
                }

                settings.StartingTemperature = settings.StartingTemperature * settings.CoolingCoefficient;
                settings.NumIterations = (int)((double)settings.NumIterations * settings.NumIterationsMultiplier);

                System.Console.Write("\n");
                System.Console.Write("\n");
                System.Console.Write("\n");
            }
            OutputData result = new OutputData();

            return result;
        }
    }
}
