using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType
{
    public class Settings
    {
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
        /// <summary>
        /// Wykorzystywana w algorytmie operacja
        /// </summary>
        public OperationType Operation { get; set; }
    }

    public enum OperationType
    {
        Operation1,
        Operation2,
        Operation3,
        Operation4
    }
}
