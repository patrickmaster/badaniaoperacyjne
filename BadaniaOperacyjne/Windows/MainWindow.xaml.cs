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
using BadaniaOperacyjne.DataType.DataFlow;
using BadaniaOperacyjne.DataType.Graph;

namespace BadaniaOperacyjne.Windows
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
            InputData input = ProblemManager.Current.GetProblem();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            IndependentArguments source = new IndependentArguments { Title = "Wykresik" };

            Series series1 = new Series { Description = "wykresik 1" };
            double range = 20;
            for (double x = -range; x < range; x += 0.1)
            {
                double y = Math.Sin(x) * Math.Cos(Math.PI/4 + x);
                series1.Values.Add(new BadaniaOperacyjne.DataType.Graph.Point { X = x, Y = y });
            }

            source.Series.Add(series1);

            Graph2.Current.Plot(source);
        }
    }
}
