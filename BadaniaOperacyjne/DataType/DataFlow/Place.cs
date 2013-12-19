using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.DataFlow
{
    public class Place
    {
        public Place()
        {
        }

        public int Number { get; private set; }

        public double Cost { get; set; }

        public static implicit operator double(Place p)
        {
            return p.Cost;
        }
        public static implicit operator Place(double v)
        {
            return new Place { Cost = v };
        }
        public static Place operator +(Place one, Place another)
        {
            return new Place { Cost = one.Cost + another.Cost };
        }
        public static Place operator -(Place one, Place another)
        {
            return new Place { Cost = one.Cost - another.Cost };
        }
        public static Place operator *(Place one, Place another)
        {
            return new Place { Cost = one.Cost * another.Cost };
        }
        public static Place operator /(Place one, Place another)
        {
            return new Place { Cost = one.Cost / another.Cost };
        }
    }
}
