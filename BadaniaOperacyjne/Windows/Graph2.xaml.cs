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
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Series;
using OxyPlot.Axes;
using BadaniaOperacyjne.DataType.Graph;

namespace BadaniaOperacyjne.Windows
{
    /// <summary>
    /// Interaction logic for Graph2.xaml
    /// </summary>
    public partial class Graph2 : Window
    {
        private static Graph2 current = null;
        public static Graph2 Current
        {
            get
            {
                if (current == null)
                    current = new Graph2();
                return current;
            }
            set { }
        }

        public PlotModel MyPlotModel { get; set; }

        private Graph2()
        {
            InitializeComponent();

            DataContext = this;
        }

        public void Plot(IndependentArguments source)
        {
            PlotModel plot = new PlotModel(source.Title);
            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Left));
            plot.Axes.Add(new OxyPlot.Axes.LinearAxis(AxisPosition.Bottom));

            foreach (BadaniaOperacyjne.DataType.Graph.Series series in source.Series)
            {
                //OxyPlot.Series.LineSeries lineSeries = new OxyPlot.Series.LineSeries(series.Description);
                OxyPlot.Series.ScatterSeries graphSeries = new OxyPlot.Series.ScatterSeries();
                graphSeries.Title = series.Description;

                foreach (BadaniaOperacyjne.DataType.Graph.Point point in series.Values)
                {
                    graphSeries.Points.Add(new ScatterPoint(point.X, point.Y, 2));
                }

                plot.Series.Add(graphSeries);
            }

            MyPlotModel = plot;

            Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            current = null;
            base.OnClosed(e);
        }
    }
}
