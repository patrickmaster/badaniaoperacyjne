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
using BadaniaOperacyjne.DataType.Graph;
using Microsoft.Research.DynamicDataDisplay; // Core functionality
using Microsoft.Research.DynamicDataDisplay.DataSources; // EnumerableDataSource
using Microsoft.Research.DynamicDataDisplay.PointMarkers; // CirclePointMarker

namespace BadaniaOperacyjne
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        private static Graph current = null;
        public static Graph Current
        {
            get
            {
                if (current == null)
                {
                    current = new Graph();
                }
                return current;
            }
            private set { }
        }

        private Graph()
        {
            InitializeComponent();
            Show();
        }
        
        private EnumerableDataSource<double> GetArguments(SimpleData source)
        {
            EnumerableDataSource<double> argumentsEnumerable = new EnumerableDataSource<double>(source.HorizontalValues);
            argumentsEnumerable.SetXMapping(x => x);

            return argumentsEnumerable;
        }

        private void DescribeGraph(SimpleData source)
        {
            horizontalAxisTitle.Content = source.HorizontalAxisTitle;
            verticalAxisTitle.Content = source.VerticalAxisTitle;
            graphTitle.Content = source.Title;
        }

        private void PlotValueLine(EnumerableDataSource<double> argumentsEnumerable, ValueLine value)
        {
            EnumerableDataSource<double> valuesEnumerable = new EnumerableDataSource<double>(value.Values);
            valuesEnumerable.SetYMapping(y => y);
            CompositeDataSource valuesComposite = new CompositeDataSource(argumentsEnumerable, valuesEnumerable);

            plotter.AddLineGraph(
                valuesComposite,
                new Pen(Brushes.Transparent, 1),
                new CirclePointMarker { Size = value.Size, Fill = value.Brush },
                new PenDescription(value.Title)
                );
        }

        public void Plot(MultipleValues source)
        {
            DescribeGraph(source);

            EnumerableDataSource<double> argumentsEnumerable = GetArguments(source);

            foreach (ValueLine value in source.VerticalValues)
            {
                PlotValueLine(argumentsEnumerable, value);
            }

            plotter.Viewport.FitToView();
        }

        public void Plot(SimpleData source)
        {
            DescribeGraph(source);

            EnumerableDataSource<double> arguments = GetArguments(source);

            PlotValueLine(arguments, source.VerticalValues);

            plotter.Viewport.FitToView();
        }

        protected override void OnClosed(EventArgs e)
        {
            current = null;
            base.OnClosed(e);
        }
    }
}
