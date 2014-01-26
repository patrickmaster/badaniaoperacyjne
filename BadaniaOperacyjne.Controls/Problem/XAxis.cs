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
    internal class XAxis : Axis
    {
        public XAxis()
        {
            //this.Height = SIZE;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.ScaleLineLengthChanged += XAxis_ScaleLineLengthChanged;
        }

        void XAxis_ScaleLineLengthChanged(object sender, ValueChangedEventArgs<double> e)
        {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            double center = CalculateCenter(size.Width);

            drawingContext.DrawLine(
                new Pen(Brushes.Black, Thickness),
                new Point(0, 0),
                new Point(size.Width * Math.Exp(Scale / 10d), 0));

            drawingContext.DrawLine(
                new Pen(Brushes.Black, Thickness),
                new Point(center, -ScaleLineLength),
                new Point(center, 0));
        }
    }
}
