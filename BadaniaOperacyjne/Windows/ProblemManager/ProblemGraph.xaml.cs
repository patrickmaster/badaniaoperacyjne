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
using BadaniaOperacyjne.Controls.Problem;
using BadaniaOperacyjne.Classes;
using BadaniaOperacyjne.DataType;
using BadaniaOperacyjne.Extensions;

namespace BadaniaOperacyjne.Windows.ProblemManager
{
    /// <summary>
    /// Interaction logic for ProblemGraph.xaml
    /// </summary>
    public partial class ProblemGraph : Window
    {
        public class LocalViewModel : ViewModel
        {
            private BadaniaOperacyjne.Controls.Problem.InputMode inputMode;
            public BadaniaOperacyjne.Controls.Problem.InputMode InputMode
            {
                get { return inputMode; }
                set
                {
                    if (value != inputMode)
                    {
                        inputMode = value;
                        NotifyPropertyChanged("InputMode");
                    }
                }
            }
        }

        public LocalViewModel VM = new LocalViewModel();

        public static RoutedCommand SwitchToInputPlaceModeCommand = new RoutedCommand();
        public static RoutedCommand SwitchToInputPetrolPlaceModeCommand = new RoutedCommand();
        public static RoutedCommand SwitchToIdleModeCommand = new RoutedCommand();

        public ProblemGraph()
        {
            DataContext = VM;

            InitializeComponent();
        }

        private void SwitchToInputPlaceModeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VM.InputMode = Controls.Problem.InputMode.InputPlace;
        }

        private void SwitchToInputPlaceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (VM.InputMode == Controls.Problem.InputMode.InputPlace)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void SwitchToInputPetrolPlaceModeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VM.InputMode = Controls.Problem.InputMode.InputPetrolPlace;
        }

        private void SwitchToInputPetrolPlaceModeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (VM.InputMode == Controls.Problem.InputMode.InputPetrolPlace)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void SwitchToIdleModeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VM.InputMode = Controls.Problem.InputMode.Idle;
        }

        private void SwitchToIdleModeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (VM.InputMode == Controls.Problem.InputMode.Idle)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            List<Place> places = graph2.GetPlaces().ToList();
            return;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
