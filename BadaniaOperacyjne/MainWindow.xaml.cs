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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BadaniaOperacyjne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSolveProblem_Click(object sender, RoutedEventArgs e)
        {
            //ProblemManager.DataType.Input problemInput = ProblemManager.ProblemManager.GetProblem();
            ProblemManager.Current.Show();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            DataType.Graph.MultipleValues graphSource = new DataType.Graph.MultipleValues();
            graphSource.HorizontalAxisTitle = "argi";
            graphSource.HorizontalValues = new List<double> { 5, 6, 7, 8 };
            graphSource.Title = "WYKRESIK";
            graphSource.VerticalAxisTitle = "wartości";
            graphSource.VerticalValues.Add(new DataType.Graph.ValueLine
            {
                Values = new List<double> { 2, 2.5, 7, 5 }
            });

            Graph.Current.Plot(graphSource);
        }
    }
}
