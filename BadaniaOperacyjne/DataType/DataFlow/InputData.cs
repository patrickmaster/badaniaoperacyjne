using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.DataFlow
{
    public class InputData
    {
        public InputData()
        {
            Places = new List<List<double>>();
        }

        public List<List<double>> Places { get; set; }
        /// <summary>
        /// Liczba miejsc (dostarczenia paczek i stacji benzynowych)
        /// </summary>
        public int NumPlaces { get; set; }
        /// <summary>
        /// Początkowa temperatura algorytmu
        /// </summary>
        public double StartingTemperature { get; set; }
        /// <summary>
        /// Końcowa temperatura algorytmu
        /// </summary>
        public double EndingTemperature { get; set; }
        /// <summary>
        /// Liczba iteracji w pierwszym bloku iteracji
        /// </summary>
        public int NumIterations { get; set; }
        /// <summary>
        /// Mnożnik liczby iteracji dla kolejnych bloków algorytmu
        /// </summary>
        public double NumIterationsMultiplier { get; set; }

        /// <summary>
        /// Współczynnik chłodzenia
        /// </summary>
        public double CoolingCoefficient { get; set; }
    }
}
