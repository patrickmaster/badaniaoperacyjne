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

namespace BadaniaOperacyjne.Controls.Problem
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Graph2 : UserControl
    {
        #region InputMode DP

        public InputMode InputMode
        {
            get { return (InputMode)GetValue(InputModeProperty); }
            set { SetValue(InputModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InputMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputModeProperty = DependencyProperty.Register(
            "InputMode", 
            typeof(InputMode), 
            typeof(Graph2), 
            new PropertyMetadata(InputMode.Idle));

        #endregion

        #region Radius DP

        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", 
            typeof(double), 
            typeof(Graph2), 
            new PropertyMetadata(5d));

        #endregion

        #region ScaleLineLength DP

        public double ScaleLineLength
        {
            get { return (double)GetValue(ScaleLineLengthProperty); }
            set { SetValue(ScaleLineLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleLineLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleLineLengthProperty = DependencyProperty.Register(
            "ScaleLineLength", 
            typeof(double), 
            typeof(Graph2), 
            new PropertyMetadata(10d));

        #endregion

        public Graph2()
        {
            //this.Clip = new RectangleGeometry(new Rect(0, 0, this.Width, this.Height));

            InitializeComponent();
        }

        public IEnumerable<Place> GetPlaces()
        {
            return graphArea.ToPlacesCollection();
        }
    }

    public enum InputMode
    {
        Idle,
        InputPlace,
        InputPetrolPlace
    }
}
