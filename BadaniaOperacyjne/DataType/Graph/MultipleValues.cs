using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType.Graph
{
    public class MultipleValues : SimpleData
    {
        public MultipleValues()
            : base()
        {
            VerticalValues = new List<ValueLine>();
        }

        /// <summary>
        /// Lista wykresów wartości
        /// </summary>
        public new List<ValueLine> VerticalValues { get; set; }
    }
}
