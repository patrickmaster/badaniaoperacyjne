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
        public Graph()
        {
            InitializeComponent();
        }

        public Graph(SimpleData source)
        {
            InitializeComponent();

            EnumerableDataSource<double> argumentsEnumerable = new EnumerableDataSource<double>(source.HorizontalValues);
            argumentsEnumerable.SetXMapping(x => x);

            EnumerableDataSource<double> valuesEnumerable = new EnumerableDataSource<double>(source.VerticalValues);
            valuesEnumerable.SetYMapping(y => y);

            //CompositeDataSource argumentsComposite = new CompositeDataSource(argumentsEnumerable);
            CompositeDataSource valuesComposite = new CompositeDataSource(argumentsEnumerable, valuesEnumerable);

            horizontalAxisTitle.Content = source.HorizontalAxisTitle;
            verticalAxisTitle.Content = source.VerticalAxisTitle;
            graphTitle.Content = source.Title;

            plotter.AddLineGraph(valuesComposite,
              new Pen(Brushes.Transparent, 1),
              new CirclePointMarker { Size = 5, Fill = Brushes.Blue },
              new PenDescription(string.IsNullOrWhiteSpace(source.VerticalAxisTitle) ? "Values" : source.VerticalAxisTitle));
            plotter.Viewport.FitToView();
        }

        public Graph(MultipleValues source)
        {
            InitializeComponent();

            horizontalAxisTitle.Content = source.HorizontalAxisTitle;
            verticalAxisTitle.Content = source.VerticalAxisTitle;
            graphTitle.Content = source.Title;

            EnumerableDataSource<double> argumentsEnumberable = new EnumerableDataSource<double>(source.HorizontalValues);
            argumentsEnumberable.SetXMapping(x => x);

            foreach (ValueLine value in source.VerticalValues)
            {
                EnumerableDataSource<double> valuesEnumerable = new EnumerableDataSource<double>(value.Values);
                valuesEnumerable.SetYMapping(y => y);
                CompositeDataSource valuesComposite = new CompositeDataSource(argumentsEnumberable, valuesEnumerable);

                plotter.AddLineGraph(
                    valuesComposite,
                    new Pen(Brushes.Transparent, 1),
                    new CirclePointMarker { Size = value.Size, Fill = value.Brush },
                    new PenDescription(value.Title)
                    );
            }

            plotter.Viewport.FitToView();
        }
    }
}
