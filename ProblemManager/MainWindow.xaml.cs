using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;

namespace ProblemManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<List<int>> ItemsList { get; set; }

        public MainWindow()
        {
            ItemsList = new ObservableCollection<List<int>>();
            ItemsList.Add(new List<int> { 1, 2, 3 });
            ItemsList.Add(new List<int> { 1, 2, 3 });
            ItemsList.Add(new List<int> { 1, 2, 3 });
            ItemsList.Add(new List<int> { 1, 2, 3 });
            ItemsList.Add(new List<int> { 1, 2, 3 });
            ItemsList.Add(new List<int> { 1, 2, 3 });

            InitializeComponent();

            dataGrid.DataContext = this;

            this.Closing += new CancelEventHandler(MainWindow_Closing);

        }

        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("Zapisać?", "Uwaga", MessageBoxButton.YesNo);
            //if (result == MessageBoxResult.No)
            //    e.Cancel = true;
        }

        public class Item
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            List<int> row = new List<int> {4,5,6};
            ItemsList.Add(row);
        }

        private void btnPokaz_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
