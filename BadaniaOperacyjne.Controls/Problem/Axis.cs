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
    internal abstract class Axis : FrameworkElement
    {
        protected Size size;
        protected const double MARGIN = 0;
        protected const double SIZE = 10;
        protected const double THICKNESS = 2;

        //public double Scale
        //{
        //    get { return (double)GetValue(ScaleProperty); }
        //    set { SetValue(ScaleProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ScaleProperty =
        //    DependencyProperty.Register("Scale", typeof(double), typeof(Axis), new PropertyMetadata(1));
        
        public Axis()
        {
            this.SizeChanged += Axis_SizeChanged;
            this.MouseWheel += Axis_MouseWheel;
        }

        void Axis_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Console.WriteLine("wheel over axis: " + e.Delta);
            //throw new NotImplementedException();
        }

        void Axis_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            size = e.NewSize;

            this.InvalidateVisual();
        }
    }
}
