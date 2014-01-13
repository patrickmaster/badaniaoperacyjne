using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType
{
    public class InputData
    {
        public InputData()
        {
            Places = new List<List<double>>();
            PetrolPlaces = new List<int>();
        }

        /// <summary>
        /// Lista miejsc do tankowania
        /// </summary>
        public List<int> PetrolPlaces { get; set; }
        /// <summary>
        /// Macierz kosztów przejazdów między miastami
        /// </summary>
        public List<List<double>> Places { get; set; }
        /// <summary>
        /// Liczba miejsc (dostarczenia paczek i stacji benzynowych)
        /// </summary>
        public int NumPlaces { get; set; }

        /// <summary>
        /// Lista operacji możliwych do wykonania w algorytmie
        /// </summary>
        public List<OperationType> Operations { get; set; }
    }

    public enum OperationType
    {
    }
}
