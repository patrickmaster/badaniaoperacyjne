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
using System.ComponentModel;
using BadaniaOperacyjne.Classes;
using BadaniaOperacyjne.Utilities;

namespace BadaniaOperacyjne.Windows.ProblemManager
{
    /// <summary>
    /// Interaction logic for PreConfiguration.xaml
    /// </summary>
    public partial class PreConfiguration : Window
    {
        public class LocalViewModel : ViewModel
        {
            protected int numPlaces;
            public int NumPlaces
            {
                get { return numPlaces; }
                set
                {
                    if (numPlaces != value)
                    {
                        numPlaces = value;
                        NotifyPropertyChanged("NumPlaces");
                    }
                }
            }
        }

        public LocalViewModel VM;

        public PreConfiguration()
        {
            VM = new LocalViewModel();

            DataContext = VM;

            InitializeComponent();
            VM.NumPlaces = 5;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationMethods.IsValid(mainForm))
            {
                System.Diagnostics.Debug.WriteLine("form is valid");
                DialogResult = true;
                Close();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("form NOT VALID");
                return;
            }
        }
    }
}
