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
using BadaniaOperacyjne.DataType;
using BadaniaOperacyjne.DataType.Graph;
using System.ComponentModel;
using BadaniaOperacyjne.Solver;
using BadaniaOperacyjne.SettingsManager;

namespace BadaniaOperacyjne.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected List<Window> windows;

        public MainWindow()
        {
            windows = new List<Window>();

            InitializeComponent();

            //solver = new Solver.Solver();
        }

        private bool? ShowDialogWindow(Window window)
        {
            windows.Add(window);

            window.Closing += delegate(object s, CancelEventArgs args)
            {
                windows.Remove(window);
            };

            return window.ShowDialog();
        }

        private void btnSolveProblem_Click(object sender, RoutedEventArgs e)
        {
            ProblemManager.PreConfiguration preConfiguration = new ProblemManager.PreConfiguration();
            int problemSize = 0;
            if (ShowDialogWindow(preConfiguration) == true)
            {
                problemSize = preConfiguration.VM.NumPlaces;
            }
            else
            {
                return;
            }

            ProblemManager.ProblemManager problemManager = new ProblemManager.ProblemManager(problemSize);
            InputData input = null;

            if (ShowDialogWindow(problemManager) == true)
            {
                // bleble
                input = problemManager.ToInputData();
            }
            else
            {
                // Handle errors
                return;
            }

            SolutionWindow solutionWindow = new SolutionWindow(input);

            ShowDialogWindow(solutionWindow);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            ShowDialogWindow(settingsWindow);
        }
    }
}
