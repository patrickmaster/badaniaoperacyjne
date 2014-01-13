using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BadaniaOperacyjne.DataType;
using System.Threading.Tasks;

namespace BadaniaOperacyjne.Solver
{
    public delegate void SolverProgressEventHandler(object sender, SolverProgressEventArgs args);
    public delegate void SolverDoneEventHandler(object sender, SolverDoneEventArgs args);
    public delegate void SolverCancelEventHandler(object sender, SolverCancelEventArgs args);

    public interface ISolver
    {
        event SolverProgressEventHandler SolverProgress;
        event SolverDoneEventHandler SolverDone;
        event SolverCancelEventHandler SolverCancel;
        bool IsBusy { get; }

        void SolveAsync(InputData input, Settings settings);
        void CancelSolve();
    }

    public class SolverBusyException : Exception
    {
    }
}
