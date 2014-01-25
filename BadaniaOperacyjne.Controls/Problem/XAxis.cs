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
            this.Height = SIZE;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawLine(
                new Pen(Brushes.Black, THICKNESS),
                new Point(MARGIN, 0),
                new Point(size.Width - MARGIN, 0));
        }
    }
}
