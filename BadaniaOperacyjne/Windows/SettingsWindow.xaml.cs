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
using BadaniaOperacyjne.Classes;
using BadaniaOperacyjne.DataType;
using BadaniaOperacyjne.SettingsManager;

namespace BadaniaOperacyjne.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public class LocalViewModel : ViewModel
        {
            public LocalViewModel()
            {
                OperationsList = new Dictionary<string, OperationType>();
                OperationsList.Add("Operacja 1", OperationType.Operation1);
                OperationsList.Add("Operacja 2", OperationType.Operation2);
            }

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
                    if (coolingCoefficient != value)
                    {
                        coolingCoefficient = value;
                        NotifyPropertyChanged("CoolingCoefficient");
                    }
                }
            }

            public Dictionary<string, OperationType> OperationsList { get; private set; }

            private OperationType operation;
            public OperationType Operation
            {
                get { return operation; }
                set
                {
                    if (value != operation)
                    {
                        operation = value;
                        NotifyPropertyChanged("Operation");
                    }
                }
            }

            private int pointsPerIterationBlock;
            public int PointsPerIterationBlock
            {
                get { return pointsPerIterationBlock; }
                set
                {
                    if (value != pointsPerIterationBlock)
                    {
                        pointsPerIterationBlock = value;
                        NotifyPropertyChanged("PointsPerIterationBlock");
                    }
                }
            }
        }

        public LocalViewModel VM { get; set; }

        private ISettingsManager settingsManager;

        public SettingsWindow()
        {
            settingsManager = new SettingsManager.SettingsManager();

            InitializeComponent();

            VM = new LocalViewModel();
            VM.PropertyChanged += VM_PropertyChanged;
            LoadSettings();

            DataContext = VM;
        }

        void VM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Operation")
            {
                Console.WriteLine("oepration changed");
            }
        }

        private void LoadSettings()
        {
            Settings settings = settingsManager.GetSettings();

            VM.StartingTemperature = settings.StartingTemperature;
            VM.EndingTemperature = settings.EndingTemperature;
            VM.NumIterations = settings.NumIterations;
            VM.NumIterationsMultiplier = settings.NumIterationsMultiplier;
            VM.CoolingCoefficient = settings.CoolingCoefficient;
            VM.Operation = settings.Operation;
            VM.PointsPerIterationBlock = settings.PointsPerIterationBlock;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();

            settings.StartingTemperature = VM.StartingTemperature;
            settings.EndingTemperature = VM.EndingTemperature;
            settings.NumIterations = VM.NumIterations;
            settings.NumIterationsMultiplier = VM.NumIterationsMultiplier;
            settings.CoolingCoefficient = VM.CoolingCoefficient;
            settings.Operation = VM.Operation;
            settings.PointsPerIterationBlock = VM.PointsPerIterationBlock;

            settingsManager.SetSettings(settings);
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
