using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadaniaOperacyjne.DataType;
using System.ComponentModel;

namespace BadaniaOperacyjne.Solver
{
    public class AsyncSolver : IAsyncSolver
    {
        private InputData input;
        private Settings settings;
        private BackgroundWorker worker;
        private ISolver solver = new Solver();

        public event SolverBeginEventHandler SolverBegin;
        public event SolverProgressEventHandler SolverProgress;
        public event SolverDoneEventHandler SolverDone;
        public event SolverCancelEventHandler SolverCancel;

        public bool IsBusy
        {
            get
            {
                if (worker != null)
                {
                    return worker.IsBusy;
                }
                else
                    return false;
            }
        }

        public AsyncSolver()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            OutputData output = solver.Solve(input, settings, worker);
            output.SolvingTime = watch.ElapsedMilliseconds;
            e.Result = output;
            watch.Stop();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            IterationBlock block = e.UserState as IterationBlock;
            SolverProgress(this, new SolverProgressEventArgs
            {
                Block = block
            });
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OutputData output = e.Result as OutputData;

            SolverDone(this, new SolverDoneEventArgs
            {
                Output = output
            });
        }

        public void SolveAsync(InputData input, Settings settings)
        {
            if (worker.IsBusy == true)
            {
                throw new SolverBusyException();
            }

            this.input = input;
            this.settings = settings;
            SolverBegin(this, new SolverBeginEventArgs
            {
                StartingTemperature = settings.StartingTemperature,
                EndingTemperature = settings.EndingTemperature
            });
            worker.RunWorkerAsync();
        }

        public void CancelSolve()
        {
            if (worker.IsBusy == true)
            {
                try
                {
                    worker.CancelAsync();
                    SolverCancel(this, new SolverCancelEventArgs
                    {
                    });
                }
                catch { }
            }
        }
    }
}
