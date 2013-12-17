using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType.DataFlow;

namespace BadaniaOperacyjne.Parser
{
    public interface IParser
    {
        InputData ReadBinaryProblemFile(string filename);
        InputData ReadUserProblemFile(string filename);
        Solution ReadSolutionFile(string filename);
        
        void WriteBinaryProblemFile(string filename, InputData problem);
        void WriteSolutionFile(string filename, Solution solution);
    }
}
