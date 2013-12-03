using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.DataFlow
{
    public class OutputData
    {
        public OutputData()
        {
            Order = new List<Place>();
            CostsHistory = new List<KeyValuePair<int, double>>();
        }

        /// <summary>
        /// Kolejność przejazdu komiwojażerka
        /// </summary>
        public List<Place> Order { get; set; }

        /// <summary>
        /// Wartości funkcji celu dla kolejnych iteracji.
        /// Kluczem jest numer kolejnej iteracji, wartością
        /// jest wartość funkcji celu w tej iteracji.
        /// </summary>
        public List<KeyValuePair<int, double>> CostsHistory { get; set; }
    }
}
