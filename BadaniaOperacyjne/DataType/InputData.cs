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

        public InputData(int places)
        {
            Places = new List<List<double>>();
            for (int i = 0; i < places; i++)
            {
                List<double> column = new List<double>();
                for (int j = 0; j < places; j++)
                {
                    column.Add(0d);
                }
                Places.Add(column);
            }

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
        /// Pojemność baku samochodu
        /// </summary>
        public double FuelCapacity { get; set; }
    }
}
