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
    internal class YAxis : Axis
    {
        public YAxis()
        {
            //this.Width = SIZE;
            this.VerticalAlignment = VerticalAlignment.Stretch;

            this.ScaleLineLengthChanged += YAxis_ScaleLineLengthChanged;
        }

        void YAxis_ScaleLineLengthChanged(object sender, ValueChangedEventArgs<double> e)
        {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            double center = CalculateCenter(size.Height);
            
            drawingContext.DrawLine(
                new Pen(Brushes.Black, Thickness),
                new Point(size.Width, 0),
                new Point(size.Width, size.Height));

            drawingContext.DrawLine(
                new Pen(Brushes.Black, Thickness),
                new Point(0, center),
                new Point(ScaleLineLength, center));
        }

    }
}
