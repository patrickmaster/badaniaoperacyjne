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
using System.Collections.ObjectModel;
using BadaniaOperacyjne.DataType.DataFlow;
using System.ComponentModel;
using BadaniaOperacyjne.Classes;

namespace BadaniaOperacyjne.Windows.ProblemManager
{
    /// <summary>
    /// Interaction logic for ProblemManager.xaml
    /// </summary>
    public partial class ProblemManager : Window
    {
        //internal ObservableCollection<List<int>> ItemsList { get; set; }
        public class LocalViewModel : ViewModel
        {
            public LocalViewModel()
            {
                ItemsList = new ObservableCollection<List<double>>();
                ItemsList.Add(new List<double> { 1, 2 });
                ItemsList.Add(new List<double> { 5, 6 });
            }

            public LocalViewModel(int dimension)
            {
                ItemsList = new ObservableCollection<List<double>>();
                for (int i = 0; i < dimension; i++)
                {
                    List<double> list = new List<double>();
                    for (int j = 0; j < dimension; j++)
                    {
                        list.Add(0);
                    }
                    ItemsList.Add(list);
                }
            }

            void ItemsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                System.Diagnostics.Debug.WriteLine("problem manager: items list changed " + e.NewItems.Count);
            }
            
            public ObservableCollection<List<double>> ItemsList { get; private set; }

            protected double startingTemperature;
            public double StartingTemperature
            {
                get { return startingTemperature; }
                set
                {
                    System.Diagnostics.Debug.WriteLine("starting temp: new value " + value);
                    if (startingTemperature != value)
                    {
                        startingTemperature = value;
                        NotifyPropertyChanged("StartingTemperature");
                    }
                }
            }

            protected double endingTemperature;
            public double EndingTemperature
            {
                get { return endingTemperature; }
                set
                {
                    if (endingTemperature != value)
                    {
                        endingTemperature = value;
                        NotifyPropertyChanged("EndingTemperature");
                    }
                }
            }

            protected int numIterations;
            public int NumIterations
            {
                get { return numIterations; }
                set
                {
                    if (numIterations != value)
                    {
                        numIterations = value;
                        NotifyPropertyChanged("NumIterations");
                    }
                }
            }

            protected double numIterationsMultiplier;
            public double NumIterationsMultiplier
            {
                get { return numIterationsMultiplier; }
                set
                {
                    System.Diagnostics.Debug.WriteLine("num iteratoins multiplier new value: " + value);
                    if (numIterationsMultiplier != value)
                    {
                        numIterationsMultiplier = value;
                        NotifyPropertyChanged("NumIterationsChanged");
                    }
                }
            }

            protected double coolingCoefficient;
            public double CoolingCoefficient
            {
                get { return coolingCoefficient; }
                set
                {
                    System.Diagnostics.Debug.WriteLine("cooling coefficient new value: " + value);
                    if (coolingCoefficient != value)
                    {
                        coolingCoefficient = value;
                        NotifyPropertyChanged("CoolingCoefficient");
                    }
                }
            }
        }

        public LocalViewModel VM { get; private set; }

        private int numPlaces;

        public ProblemManager(int size)
        {
            numPlaces = size;

            VM = new LocalViewModel(size);

            DataContext = VM;
            InitializeComponent();

            //dataGrid.DataContext = VM;
        }

        public InputData ToInputData()
        {
            InputData result = new InputData();

            foreach (List<double> column in VM.ItemsList)
            {
                result.Places.Add(column);
            }

            result.NumPlaces = numPlaces;
            result.StartingTemperature = VM.StartingTemperature;
            result.EndingTemperature = VM.EndingTemperature;
            result.NumIterations = VM.NumIterations;
            result.NumIterationsMultiplier = VM.NumIterationsMultiplier;
            result.CoolingCoefficient = VM.CoolingCoefficient;

            return result;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";

            foreach (List<double> list in VM.ItemsList)
            {
                msg += string.Join(", ", list);
                msg += Environment.NewLine;
            }

            MessageBox.Show(msg);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            string msg = "num places: " + numPlaces + Environment.NewLine;
            msg += "Starting temp: " + VM.StartingTemperature + Environment.NewLine;
            msg += "Ending temp: " + VM.EndingTemperature + Environment.NewLine;
            MessageBox.Show(msg);
            DialogResult = true;
            Close();
        }
    }
}
