using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class Content : FrameworkElement
    {
        Size _size;
        public Content()
        {
            this.SizeChanged += (o, e) =>
                {
                    _size = e.NewSize;
                    this.InvalidateVisual(); // cause a render
                };
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(
                Brushes.Blue, 
                new Pen(Brushes.Red, 2), 
                new Rect(1, 1, _size.Width / 2, _size.Height / 2));
            drawingContext.DrawEllipse(
                Brushes.Green, 
                new Pen(Brushes.Yellow, 2), 
                center: new Point(_size.Width / 2, _size.Height / 2), 
                radiusX: _size.Width / 4, 
                radiusY: _size.Height / 4);
            var txt = new FormattedText(
                "Foobar",
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Courier new"),
                21,
                Brushes.Red);
            drawingContext.DrawText(txt, 
                new Point(_size.Width / 2, _size.Height / 2)
                );
        }
    }
}
