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

namespace BadaniaOperacyjne
{
    /// <summary>
    /// Interaction logic for ProblemManager.xaml
    /// </summary>
    public partial class ProblemManager : Window
    {
        private static ProblemManager current = null;
        public static ProblemManager Current
        {
            get
            {
                if (current == null)
                {
                    current = new ProblemManager();
                }
                return current;
            }
            private set { }
        }

        //internal ObservableCollection<List<int>> ItemsList { get; set; }
        public ViewModel VM { get; private set; }

        private ProblemManager()
        {
            VM = new ViewModel();

            VM.ItemsList = new ObservableCollection<List<int>>();
            VM.ItemsList.Add(new List<int> { 1, 2, 3 });
            VM.ItemsList.Add(new List<int> { 1, 2, 3 });
            VM.ItemsList.Add(new List<int> { 1, 2, 3 });
            VM.ItemsList.Add(new List<int> { 1, 2, 3 });

            InitializeComponent();

            dataGrid.DataContext = VM;
        }

        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            List<int> row = new List<int> { 4, 5, 6 };
            VM.ItemsList.Add(row);
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";

            foreach (List<int> list in VM.ItemsList)
            {
                foreach (int item in list)
                {
                    msg += item + " ";
                }
                msg += Environment.NewLine;
            }

            MessageBox.Show(msg);
        }

        protected override void OnClosed(EventArgs e)
        {
            current = null;
            base.OnClosed(e);
        }

        public class ViewModel
        {
            public ViewModel()
            {
                ItemsList = new ObservableCollection<List<int>>();
            }
            public ObservableCollection<List<int>> ItemsList { get; set; }
        }
    }
}
