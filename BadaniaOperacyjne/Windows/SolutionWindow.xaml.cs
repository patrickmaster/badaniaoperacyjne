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

namespace BadaniaOperacyjne.Windows
{
    /// <summary>
    /// Interaction logic for Solution.xaml
    /// </summary>
    public partial class SolutionWindow : Window
    {
        private OutputData output;

        public PlotModel GraphPlotModel { get; set; }

        private SolutionWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        public SolutionWindow(OutputData output)
        {
            // TODO: Complete member initialization
            this.output = output;

            InitializeComponent();
            DataContext = this; 
            
            Plot(output);
        }
        
        private void Plot(OutputData data)
        {
            PlotModel plot = new PlotModel();

            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Left));
            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Bottom));

            OxyPlot.Series.ScatterSeries graphSeries = new OxyPlot.Series.ScatterSeries();

            foreach (Iteration i in data.Iterations)
            {
                graphSeries.Points.Add(new ScatterPoint(i.IterationNumber, i.Cost, 2));
            }

            plot.Series.Add(graphSeries);

            GraphPlotModel = plot;
        }
    }
}
