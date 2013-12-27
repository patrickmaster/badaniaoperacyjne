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
        }

        public LocalViewModel VM { get; set; }

        private ISettingsManager settingsManager;

        public SettingsWindow()
        {
            settingsManager = new SettingsManager.SettingsManager();

            InitializeComponent();

            VM = new LocalViewModel();
            LoadSettings();

            DataContext = VM;
        }

        private void LoadSettings()
        {
            Settings settings = settingsManager.GetSettings();

            VM.StartingTemperature = settings.StartingTemperature;
            VM.EndingTemperature = settings.EndingTemperature;
            VM.NumIterations = settings.NumIterations;
            VM.NumIterationsMultiplier = settings.NumIterationsMultiplier;
            VM.CoolingCoefficient = settings.CoolingCoefficient;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();

            settings.StartingTemperature = VM.StartingTemperature;
            settings.EndingTemperature = VM.EndingTemperature;
            settings.NumIterations = VM.NumIterations;
            settings.NumIterationsMultiplier = VM.NumIterationsMultiplier;
            settings.CoolingCoefficient = VM.CoolingCoefficient;

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
