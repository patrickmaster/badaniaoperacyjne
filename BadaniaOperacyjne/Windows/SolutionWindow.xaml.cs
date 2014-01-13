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

namespace BadaniaOperacyjne.Windows
{
    /// <summary>
    /// Interaction logic for Solution.xaml
    /// </summary>
    public partial class SolutionWindow : Window
    {
        public class LocalViewModel : ViewModel
        {
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
        }

        public LocalViewModel VM;

        private const int LINES_THICKNESS = 1;

        private InputData input;

        private ISettingsManager settingsManager;

        private ISolver solver;

        public SolutionWindow(InputData input)
        {
            solver = new MockSolver();
            settingsManager = new SettingsManager.SettingsManager();
            VM = new LocalViewModel();

            this.input = input;

            InitializeComponent();
            DataContext = VM;

            solver.SolverProgress += solver_SolverProgress;
            solver.SolverDone += solver_SolverDone;
            solver.SolverCancel += solver_SolverCancel;

            InitializeGraph();

            solver.SolveAsync(input, settingsManager.GetSettings());
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            solver.CancelSolve();
            base.OnClosing(e);
        }

        private void InitializeGraph()
        {
            PlotModel plot = new PlotModel();

            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Left));
            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Bottom));

            OxyPlot.Series.ScatterSeries costSeries = new OxyPlot.Series.ScatterSeries();
            OxyPlot.Series.StairStepSeries progressionSeries = new OxyPlot.Series.StairStepSeries(OxyColor.FromRgb(0, 255, 0), LINES_THICKNESS, "Poprawa");
            OxyPlot.Series.StairStepSeries regressionSeries = new OxyPlot.Series.StairStepSeries(OxyColor.FromRgb(255, 0, 0), LINES_THICKNESS, "Pogorszenie");

            plot.Series.Add(costSeries);
            plot.Series.Add(progressionSeries);
            plot.Series.Add(regressionSeries);

            VM.GraphPlotModel = plot;
        }

        void solver_SolverCancel(object sender, SolverCancelEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("solver cancelled");
        }

        void solver_SolverDone(object sender, SolverDoneEventArgs args)
        {
            try
            {
                VM.PlacesOrder = string.Join(",", args.Output.Order);
                VM.TotalCost = args.Output.TotalCost;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("some error finishing solver with UI");
            }
            System.Diagnostics.Debug.WriteLine("solver completed");
        }

        int count = 0;
        void solver_SolverProgress(object sender, SolverProgressEventArgs args)
        {
            if (VM.GraphPlotModel.Series.Count <= 0)
                return;

            IterationBlock iterationBlock = args.Block;

            OxyPlot.Series.ScatterSeries costSeries = VM.GraphPlotModel.Series[0] as OxyPlot.Series.ScatterSeries;
            OxyPlot.Series.StairStepSeries progressionSeries = VM.GraphPlotModel.Series[1] as OxyPlot.Series.StairStepSeries;
            OxyPlot.Series.StairStepSeries regressionSeries = VM.GraphPlotModel.Series[2] as OxyPlot.Series.StairStepSeries;

            foreach (Iteration iteration in iterationBlock.Iterations)
            {
                costSeries.Points.Add(new ScatterPoint(/*iteration.IterationNumber*/ count, iteration.Cost, LINES_THICKNESS));
            }
            progressionSeries.Points.Add(new ScatterPoint(count, iterationBlock.ProgressionCount));
            regressionSeries.Points.Add(new ScatterPoint(count, iterationBlock.RegressionCount));

            VM.GraphPlotModel.RefreshPlot(true);
            count++;
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
