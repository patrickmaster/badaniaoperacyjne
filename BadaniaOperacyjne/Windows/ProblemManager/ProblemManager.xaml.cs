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
using BadaniaOperacyjne.DataType;
using System.ComponentModel;
using BadaniaOperacyjne.Classes;
using System.Windows.Controls.Primitives;
using BadaniaOperacyjne.Parser;
using Microsoft.Win32;

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
            public LocalViewModel(int dimension)
            {
                Construct(dimension);
            }

            public void Construct(int dimension)
            {
                List<List<double>> itemsList = new List<List<double>>();
                for (int i = 0; i < dimension; i++)
                {
                    List<double> list = new List<double>();
                    for (int j = 0; j < dimension; j++)
                    {
                        list.Add(0);
                    }
                    itemsList.Add(list);
                }

                ItemsList = itemsList;

                ConstructNonNestedFields();
            }

            public void Construct(List<List<double>> list)
            {
                ItemsList = list;
                ConstructNonNestedFields();
            }

            /// <summary>
            /// Metoda resetuje wszystkie "płaskie" pola. Powinna być
            /// wywoływana na końcu, po innych ustawieniach (szczególnie
            /// po załadowaniu zawartości do ItemsList, gdyż wylicza ilość
            /// miejsc na podstawie ItemsList).
            /// </summary>
            private void ConstructNonNestedFields()
            {
                PetrolPlaces = new ObservableCollection<int>();
                CurrentFile = null;
                NumPlaces = ItemsList.Count;
            }

            public int NumPlaces { get; set; }

            private List<List<double>> itemsList;
            public List<List<double>> ItemsList
            {
                get { return itemsList; }
                set
                {
                    if (value != itemsList)
                    {
                        itemsList = value;
                        NotifyPropertyChanged("ItemsList");
                    }
                }
            }

            public ObservableCollection<int> PetrolPlaces { get; set; }

            private string currentFile;
            public string CurrentFile
            {
                get { return currentFile; }
                set
                {
                    if (currentFile != value)
                    {
                        currentFile = value;
                        NotifyPropertyChanged("CurrentFile");
                        NotifyPropertyChanged("WindowTitle");
                    }
                }
            }

            public string WindowTitle
            {
                get
                {
                    if (string.IsNullOrEmpty(currentFile))
                        return "Manager problemów";
                    else
                        return currentFile + " - Manager problemów";
                }
            }

            private double fuelCapacity;
            public double FuelCapacity
            {
                get { return fuelCapacity; }
                set
                {
                    if (value != fuelCapacity)
                    {
                        fuelCapacity = value;
                        NotifyPropertyChanged("FuelCapacity");
                    }
                }
            }
        }

        public LocalViewModel VM { get; private set; }

        private IParser parser;

        private bool isUnsaved = false;

        private ProblemManager()
        {
            this.Closing += ProblemManager_Closing;
        }

        public ProblemManager(int problemSize) : this()
        {
            parser = new Parser.Parser();

            VM = new LocalViewModel(problemSize);
            VM.PropertyChanged += ViewModelChanged;
            VM.PetrolPlaces.CollectionChanged += ViewModelChanged;

            DataContext = VM;
            InitializeComponent();
            dataGrid.CellEditEnding += ViewModelChanged;
        }

        void ViewModelChanged(object sender, EventArgs e)
        {
            isUnsaved = true;
        }

        void ProblemManager_Closing(object sender, CancelEventArgs e)
        {
            if (isUnsaved == true)
            {
                MessageBoxResult result = MessageBox.Show("Zapisać zmiany?", "Uwaga", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        //SaveAsCommand.Execute(null, null);
                        if (VM.CurrentFile == null)
                        {
                            ApplicationCommands.SaveAs.Execute(null, null);
                        }
                        else
                        {
                            ApplicationCommands.Save.Execute(null, null);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }

        }

        void openBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "tspf";
            openFileDialog.Filter = "Plik z problemem komiwojażera (*.tspf)|*.tspf";

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                try
                {
                    InputData input = parser.ReadBinaryProblemFile(path);
                    VM.Construct(input.Places);
                    VM.PetrolPlaces.CollectionChanged += ViewModelChanged;
                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        foreach (int petrolPlace in input.PetrolPlaces)
                        {
                            TogglePlaceType(petrolPlace);
                        }
                        isUnsaved = false;
                    }, System.Windows.Threading.DispatcherPriority.Render);
                    VM.CurrentFile = path;
                    isUnsaved = false;
                }
                catch
                {
                    MessageBox.Show("Nie udało się otworzyć podanego pliku", "Uwaga");
                }
            }
        }

        void closeBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void saveAsBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "tspf";
            saveFileDialog.Filter = "Plik z problemem komiwojażera (*.tspf)|*.tspf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;
                try
                {
                    parser.WriteBinaryProblemFile(path, ToInputData());
                    VM.CurrentFile = path;
                    isUnsaved = false;
                }
                catch
                {
                    MessageBox.Show("Nie udało się zapisać pliku", "Uwaga");
                }
            }
        }

        void saveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (VM.CurrentFile != null)
            {
                parser.WriteBinaryProblemFile(VM.CurrentFile, ToInputData());
                isUnsaved = false;
            }
            else
            {
                saveAsBinding_Executed(sender, e);
            }
        }

        void newBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PreConfiguration preConfiguration = new PreConfiguration();
            int problemSize = 0;
            if (preConfiguration.ShowDialog() == true)
            {
                problemSize = preConfiguration.VM.NumPlaces;
                VM.Construct(problemSize);
            }
        }

        public InputData ToInputData()
        {
            InputData result = new InputData();

            foreach (List<double> column in VM.ItemsList)
            {
                result.Places.Add(column);
            }

            result.PetrolPlaces = VM.PetrolPlaces.ToList();
            result.NumPlaces = VM.NumPlaces;
            result.FuelCapacity = VM.FuelCapacity;

            return result;
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private bool TogglePlaceType(int number)
        {
            bool isPetrolPlaceNow = TogglePlaceTypeInList(number);

            DataGridColumnHeader columnHeader = GetColumnHeader(number, dataGrid);
            DataGridRowHeader rowHeader = GetRowHeader(number, dataGrid);

            if (isPetrolPlaceNow)
            {
                ToggleColorHeader(columnHeader, petrolPlaceBrush);
                ToggleColorHeader(rowHeader, petrolPlaceBrush);
            }
            else
            {
                ToggleColorHeader(columnHeader, null);
                ToggleColorHeader(rowHeader, null);
            }

            return isPetrolPlaceNow;
        }

        /// <summary>
        /// Zmienia typ miejsca o zadanym kolejnym numerze.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// Prawda, jeśli element został dodany do listy stacji
        /// benzynowych. Fałsz, jeśli został dodany do listy miejsc dostarczenia
        /// przesyłki.
        /// </returns>
        private bool TogglePlaceTypeInList(int number)
        {
            if (number < 0 || number >= VM.NumPlaces)
                throw new IndexOutOfRangeException("Jest tylko " + VM.NumPlaces + " miast.");
            
            if (VM.PetrolPlaces.Contains(number))
            {
                VM.PetrolPlaces.Remove(number);
                return false;
            }
            else
            {
                VM.PetrolPlaces.Add(number);
                return true;
            }
        }

        private Brush petrolPlaceBrush = Brushes.Aqua;

        private void dataGrid_ColumnHeader_Click(object sender, RoutedEventArgs args)
        {
            DataGridColumnHeader header = sender as DataGridColumnHeader;

            int number = int.Parse(header.Content.ToString());
            TogglePlaceType(number);
        }

        private void dataGrid_RowHeader_Click(object sender, RoutedEventArgs args)
        {
            DataGridRowHeader header = sender as DataGridRowHeader;
            int number = int.Parse(header.Content.ToString());
            TogglePlaceType(number);
        }

        private void ToggleColorHeader(ButtonBase element, Brush newColor)
        {
            if (element.Background == null)
            {
                element.Background = newColor;
            }
            else
            {
                element.Background = null;
            }
        }

        private static DataGridRowHeader GetRowHeader(int number, DataGrid dataGrid)
        {
            List<DataGridRowHeader> rowHeaders = GetVisualChildCollection<DataGridRowHeader>(dataGrid);
            try
            {
                return rowHeaders.ElementAt(number);
            }
            catch
            {
                return null;
            }
        }

        private static DataGridColumnHeader GetColumnHeader(int number, DataGrid dataGrid)
        {
            List<DataGridColumnHeader> columnHeaders = GetVisualChildCollection<DataGridColumnHeader>(dataGrid);
            try
            {
                return columnHeaders.ElementAt(number + 1);
            }
            catch
            {
                return null;
            }
        }

        private static DataGridColumnHeader GetColumnHeaderFromColumn(DataGridColumn column, DataGrid dataGrid)
        {
            // dataGrid is the name of your DataGrid. In this case Name="dataGrid"
            List<DataGridColumnHeader> columnHeaders = GetVisualChildCollection<DataGridColumnHeader>(dataGrid);
            foreach (DataGridColumnHeader columnHeader in columnHeaders)
            {
                if (columnHeader.Column == column)
                {
                    return columnHeader;
                }
            }
            return null;
        }

        private static List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

    }
}
