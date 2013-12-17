using System;
using System.Collections;
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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BadaniaOperacyjne.Controls
{
    /// <summary>
    /// Interaction logic for Matrix.xaml
    /// </summary>
    public partial class Matrix : UserControl
    {
        public Matrix()
        {
            //DataContext = this;
            InitializeComponent();
        }

        #region ItemsList Property
        public static readonly DependencyProperty ItemsListProperty =
            DependencyProperty.Register("ItemsList", typeof(IEnumerable), typeof(Matrix), new PropertyMetadata(new PropertyChangedCallback(ItemsListChanged)));
        public IEnumerable ItemsList
        {
            get { return GetValue(ItemsListProperty) as IEnumerable; }
            set { SetValue(ItemsListProperty, value); }
        }
        private void ItemsListChanged(object value)
        {
            System.Diagnostics.Debug.WriteLine("matrix: items list changed " + value);
            if (ItemsList != null)
            {
                //ItemsList.CollectionChanged += ItemsList_CollectionChanged;
                System.Diagnostics.Debug.WriteLine("got something");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("got null");
            }
        }

        void ItemsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("matrix: current items list collection changed");
        }
        private static void ItemsListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Matrix)d).ItemsListChanged(e.NewValue);
        }
        #endregion

        #region Title Property
        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.Register("Title", typeof(string), typeof(Matrix), new PropertyMetadata("", new PropertyChangedCallback(TitleChanged)));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        private void TitleChanged(string title)
        {
            System.Diagnostics.Debug.WriteLine("matrix: title changed to: " + title);
        }
        private static void TitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Matrix)d).TitleChanged((string)e.NewValue);
        }
        #endregion
    }
}
