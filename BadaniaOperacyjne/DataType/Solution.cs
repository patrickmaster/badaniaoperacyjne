using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadaniaOperacyjne.DataType
{
    public class Solution
    {
        public Solution()
        {
            Output = new OutputData();
            Input = new InputData();
        }

        public OutputData Output { get; set; }
        public InputData Input { get; set; }
    }
}
