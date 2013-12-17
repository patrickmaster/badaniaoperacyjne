using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.DataFlow
{
    public class Place
    {
        public Place(int number)
        {
            Number = number;
        }

        public int Number { get; private set; }
    }
}
