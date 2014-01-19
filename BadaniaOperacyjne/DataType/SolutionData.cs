using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType
{
    public class SolutionData
    {
        public SolutionData(InputData input, OutputData output)
        {
            Input = input;
            Output = output;
        }

        public OutputData Output { get; private set; }
        public InputData Input { get; private set; }
    }
}
