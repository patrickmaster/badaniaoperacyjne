using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BadaniaOperacyjne.DataType;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot.Axes;
using OxyPlot;
using BadaniaOperacyjne.Solver;
using BadaniaOperacyjne.SettingsManager;
using BadaniaOperacyjne.Classes;
using Microsoft.Win32;
using BadaniaOperacyjne.Parser;

namespace BadaniaOperacyjne.Windows
{
    /// <summary>
    /// Interaction logic for Solution.xaml
    /// </summary>
    public partial class SolutionWindow : Window
    {
        private const int LINES_THICKNESS = 1;
        private const int DISPLAYED_COSTS_PER_BLOCK = 50;

        private bool isUnsaved = false;
        private InputData input;
        private SolutionData solution;
        private ISettingsManager settingsManager = new SettingsManager.SettingsManager();
        private IAsyncSolver solver = new AsyncSolver();
        private IParser parser = new Parser.Parser();

        public class LocalViewModel : ViewModel
        {
            public void Reset()
            {
                PlacesOrder = "";
                TotalCost = 0;
                TotalProgressions = 0;
                TotalRegressions = 0;
                IterationBlocksNumber = 0;
                MinCost = 0;
                MaxCost = 0;
                StartingTemperature = 0;
                EndingTemperature = 0;
                CurrentTemperature = 0;
                SolvingTime = 0;
            }

            private string placesOrder;
            public string PlacesOrder
            {
                get { return placesOrder; }
                set
                {
                    if (value != placesOrder)
                    {
                        placesOrder = value;
                        NotifyPropertyChanged("PlacesOrder");
                    }
                }
            }

            private double totalCost;
            public double TotalCost
            {
                get { return totalCost; }
                set
                {
                    if (value != totalCost)
                    {
                        totalCost = value;
                        NotifyPropertyChanged("TotalCost");
                    }
                }
            }

            public PlotModel GraphPlotModel { get; set; }

            private int totalProgressions;
            public int TotalProgressions
            {
                get { return totalProgressions; }
                set
                {
                    if (value != totalProgressions)
                    {
                        totalProgressions = value;
                        NotifyPropertyChanged("TotalProgressions");
                    }
                }
            }

            private int totalRegressions;
            public int TotalRegressions
            {
                get { return totalRegressions; }
                set
                {
                    if (value != totalRegressions)
                    {
                        totalRegressions = value;
                        NotifyPropertyChanged("TotalRegressions");
                    }
                }
            }

            private int iterationBlocksNumber;
            public int IterationBlocksNumber
            {
                get { return iterationBlocksNumber; }
                set
                {
                    if (value != iterationBlocksNumber)
                    {
                        iterationBlocksNumber = value;
                        NotifyPropertyChanged("IterationBlocksNumber");
                    }
                }
            }

            private double minCost;
            public double MinCost
            {
                get { return minCost; }
                set
                {
                    if (value != minCost)
                    {
                        minCost = value;
                        NotifyPropertyChanged("MinCost");
                    }
                }
            }

            private double maxCost;
            public double MaxCost
            {
                get { return maxCost; }
                set
                {
                    if (value != maxCost)
                    {
                        maxCost = value;
                        NotifyPropertyChanged("MaxCost");
                    }
                }
            }

            private double startingTemperature;
            public double StartingTemperature
            {
                get { return startingTemperature; }
                set
                {
                    if (value != startingTemperature)
                    {
                        startingTemperature = value;
                        NotifyPropertyChanged("MinTemperature");
                    }
                }
            }

            private double endingTemperature;
            public double EndingTemperature
            {
                get { return endingTemperature; }
                set
                {
                    if (value != endingTemperature)
                    {
                        endingTemperature = value;
                        NotifyPropertyChanged("MaxTemperature");
                    }
                }
            }

            private double currentTemperature;
            public double CurrentTemperature
            {
                get { return currentTemperature; }
                set
                {
                    if (value != currentTemperature)
                    {
                        currentTemperature = value;
                        NotifyPropertyChanged("CurrentTemperature");
                    }
                }
            }

            private double solvingTime;
            public double SolvingTime
            {
                get { return solvingTime; }
                set
                {
                    if (value != solvingTime)
                    {
                        solvingTime = value;
                        NotifyPropertyChanged("SolvingTime");
                    }
                }
            }
        }

        public LocalViewModel VM;
        public static RoutedCommand SolveAgainSolverCommand = new RoutedCommand();
        public static RoutedCommand CancelSolverCommand = new RoutedCommand();
        public static RoutedCommand SaveAsCommand = new RoutedCommand();
        public static RoutedCommand CloseCommand = new RoutedCommand();

        private SolutionWindow()
        {
            VM = new LocalViewModel();

            InitializeComponent();

            DataContext = VM;

            this.Closing += SolutionWindow_Closing;

            solver.SolverBegin += solver_SolverBegin;
            solver.SolverProgress += solver_SolverProgress;
            solver.SolverDone += solver_SolverDone;
            //solver.SolverCancel += solver_SolverCancel;

            InitializeGraph();
        }

        public SolutionWindow(InputData input) : this()
        {
            this.input = input;

            isUnsaved = true;

            solver.SolveAsync(input, settingsManager.GetSettings());
        }

        public SolutionWindow(SolutionData solution) : this()
        {
            this.solution = solution;
            this.input = solution.Input;

            PlotOutput(solution.Output);
        }

        void SolutionWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (solver.IsBusy == true)
            {
                MessageBoxResult result = MessageBox.Show("Program jest w trakcie rozwiązywania problemu. Czy chcesz przerwać rozwiązywanie i zamknąć program?", "Uwaga", MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        solver.CancelSolve();
                        break;
                    case MessageBoxResult.No:
                        e.Cancel = true;
                        break;
                }
            }
            else if (isUnsaved == true)
            {
                MessageBoxResult result = MessageBox.Show("Zapisać zmiany?", "Uwaga", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SaveAsCommand.Execute(null, null);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                solver.CancelSolve();
            }
            catch { }

            base.OnClosed(e);
        }

        private void InitializeGraph()
        {
            PlotModel plot = new PlotModel();

            OxyPlot.Axes.LinearAxis costAxis = new OxyPlot.Axes.LinearAxis(AxisPosition.Left);
            costAxis.Key = "cost";
            OxyPlot.Axes.LinearAxis progressionAxis = new OxyPlot.Axes.LinearAxis(AxisPosition.Right);
            progressionAxis.Key = "progression";

            plot.Axes.Add(costAxis); 
            plot.Axes.Add(progressionAxis);
            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Bottom));

            OxyPlot.Series.ScatterSeries costSeries = new OxyPlot.Series.ScatterSeries();
            costSeries.YAxisKey = "cost";
            OxyPlot.Series.StairStepSeries progressionSeries = new OxyPlot.Series.StairStepSeries(OxyColor.FromRgb(0, 255, 0), LINES_THICKNESS, "Poprawa");
            progressionSeries.YAxisKey = "progression";
            OxyPlot.Series.StairStepSeries regressionSeries = new OxyPlot.Series.StairStepSeries(OxyColor.FromRgb(255, 0, 0), LINES_THICKNESS, "Pogorszenie");
            regressionSeries.YAxisKey = "progression";

            plot.Series.Add(costSeries);
            plot.Series.Add(progressionSeries);
            plot.Series.Add(regressionSeries);

            VM.GraphPlotModel = plot;
        }

        private void ClearGraph()
        {
            OxyPlot.Series.ScatterSeries costSeries = VM.GraphPlotModel.Series[0] as OxyPlot.Series.ScatterSeries;
            OxyPlot.Series.StairStepSeries progressionSeries = VM.GraphPlotModel.Series[1] as OxyPlot.Series.StairStepSeries;
            OxyPlot.Series.StairStepSeries regressionSeries = VM.GraphPlotModel.Series[2] as OxyPlot.Series.StairStepSeries;

            costSeries.Points.Clear();
            progressionSeries.Points.Clear();
            regressionSeries.Points.Clear();

            VM.GraphPlotModel.RefreshPlot(true);
        }

        void solver_SolverDone(object sender, SolverDoneEventArgs args)
        {
            OutputData output = args.Output;

            switch (output.State)
            {
                case OutputState.Done:
                    try
                    {
                        VM.PlacesOrder = string.Join(",", args.Output.Solution);
                        VM.TotalCost = args.Output.TotalCost;
                        VM.SolvingTime = args.Output.SolvingTime / 1000;
                        solution = new SolutionData(input, args.Output);
                    }
                    catch
                    {
                        MessageBox.Show("Błąd podczas wczytywania danych wynikowych algorytmu", "Uwaga");
                    }
                    break;
                case OutputState.Cancelled:
                    break;
                case OutputState.NoSolution:
                    MessageBox.Show("Brak rozwiązania");
                    break;
            }
            CommandManager.InvalidateRequerySuggested();
        }

        int count = 0;

        void solver_SolverBegin(object sender, SolverBeginEventArgs args)
        {
            count = 0;
            VM.StartingTemperature = args.StartingTemperature;
            VM.EndingTemperature = args.EndingTemperature;
        }

        void solver_SolverProgress(object sender, SolverProgressEventArgs args)
        {
            if (VM.GraphPlotModel.Series.Count <= 0)
                return;

            IterationBlock iterationBlock = args.Block;

            OxyPlot.Series.ScatterSeries costSeries = VM.GraphPlotModel.Series[0] as OxyPlot.Series.ScatterSeries;
            OxyPlot.Series.StairStepSeries progressionSeries = VM.GraphPlotModel.Series[1] as OxyPlot.Series.StairStepSeries;
            OxyPlot.Series.StairStepSeries regressionSeries = VM.GraphPlotModel.Series[2] as OxyPlot.Series.StairStepSeries;

            List<double> costs = iterationBlock.Iterations.Select(x => x.Cost).ToList();
            double currentMin = costs.Min();
            double currentMax = costs.Max();
            if (currentMin < VM.MinCost || VM.MinCost == 0)
                VM.MinCost = currentMin;
            if (currentMax > VM.MaxCost)
                VM.MaxCost = currentMax;

            List<double> reducedCosts = ReduceCollection(costs, DISPLAYED_COSTS_PER_BLOCK, x => x.Average()).ToList();
            for(int i = 0; i < reducedCosts.Count; i++)
            {
                costSeries.Points.Add(new ScatterPoint(/*iteration.IterationNumber*/ (double)count + (double)i/(double)DISPLAYED_COSTS_PER_BLOCK, reducedCosts[i], LINES_THICKNESS));
            }
            progressionSeries.Points.Add(new ScatterPoint(count , iterationBlock.ProgressionCount));
            regressionSeries.Points.Add(new ScatterPoint(count , iterationBlock.RegressionCount));

            VM.TotalProgressions += iterationBlock.ProgressionCount;
            VM.TotalRegressions += iterationBlock.RegressionCount;
            VM.IterationBlocksNumber++;
            VM.CurrentTemperature = iterationBlock.CurrentTemperature;

            VM.GraphPlotModel.RefreshPlot(true);
            count++;
        }

        private IEnumerable<double> ReduceCollection(IEnumerable<double> collection, int desiredCount, Func<IEnumerable<double>,double> reducer)
        {
            List<double> result = new List<double>();
            int elementsPerBlock = collection.Count() / desiredCount;
            int count = 0;
            List<double> container = new List<double>();

            while (count < collection.Count())
            {
                //double sum = 0;
                int i = 0;
                for (i = 0; i < elementsPerBlock && count < collection.Count(); i++, count++)
                {
                    //sum += collection.ElementAt(count)
                    container.Add(collection.ElementAt(count));
                }

                //result.Add(sum / (double)i);
                result.Add(reducer(container));
                container.Clear();
            }
            return result;
        }

        private void PlotOutput(OutputData outputData)
        {
            for (int count = 0; count < outputData.Iterations.Count; count++)
            {
                IterationBlock iterationBlock = outputData.Iterations[count];

                OxyPlot.Series.ScatterSeries costSeries = VM.GraphPlotModel.Series[0] as OxyPlot.Series.ScatterSeries;
                OxyPlot.Series.StairStepSeries progressionSeries = VM.GraphPlotModel.Series[1] as OxyPlot.Series.StairStepSeries;
                OxyPlot.Series.StairStepSeries regressionSeries = VM.GraphPlotModel.Series[2] as OxyPlot.Series.StairStepSeries;

                List<double> costs = iterationBlock.Iterations.Select(x => x.Cost).ToList();
                List<double> reducedCosts = ReduceCollection(costs, DISPLAYED_COSTS_PER_BLOCK, x => x.Average()).ToList();

                for (int i = 0; i < reducedCosts.Count; i++)
                {
                    costSeries.Points.Add(new ScatterPoint(/*iteration.IterationNumber*/ (double)count + (double)i / (double)DISPLAYED_COSTS_PER_BLOCK, reducedCosts[i], LINES_THICKNESS));
                }
                progressionSeries.Points.Add(new ScatterPoint(count, iterationBlock.ProgressionCount));
                regressionSeries.Points.Add(new ScatterPoint(count, iterationBlock.RegressionCount));

                VM.TotalProgressions += iterationBlock.ProgressionCount;
                VM.TotalRegressions += iterationBlock.RegressionCount;
            }

            VM.GraphPlotModel.RefreshPlot(false);
        }

        private void CancelSolverCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            solver.CancelSolve();
        }

        private void CancelSolverCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (solver.IsBusy == true)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void SaveAsCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "tssf";
            dialog.Filter = "Plik z rozwiązaniem problemu komiwojażera (*.tssf)|*.tssf";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    parser.WriteSolutionFile(dialog.FileName, solution);
                    isUnsaved = false;
                }
                catch
                {
                    MessageBox.Show("Nie udało się zapisać rozwiązania", "Uwaga");
                }
            }
        }

        private void SaveAsCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (solver.IsBusy == true || solution == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void SolveAgainSolverCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ClearGraph();
            VM.Reset();

            solver.SolveAsync(this.input, settingsManager.GetSettings());
            CommandManager.InvalidateRequerySuggested();
        }

        private void SolveAgainSolverCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (solver.IsBusy == false && this.input != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        /*
        private void Plot(OutputData data)
        {
            PlotModel plot = new PlotModel();

            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Left));
            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Bottom));

            OxyPlot.Series.ScatterSeries graphSeries = new OxyPlot.Series.ScatterSeries();

            foreach (IterationBlock i in data.Iterations)
            {
                graphSeries.Points.Add(new ScatterPoint(i.IterationNumber, i.Cost, 2));
            }

            plot.Series.Add(graphSeries);

            GraphPlotModel = plot;
        }
         */
    }
}
