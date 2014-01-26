using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace BadaniaOperacyjne.Controls.Problem
{
    internal class PackagePlaceVisual : PlaceVisual
    {
        protected static Brush outline = Brushes.Brown;
        protected static Brush background = Brushes.Coral;

        public PackagePlaceVisual(Point position) : base(position)
        {
            this.MouseLeftButtonDown += PackagePlace_MouseLeftButtonDown;
        }

        void PackagePlace_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Console.WriteLine("clicked on package");
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (Selected)
            {
                drawingContext.DrawEllipse(
                    background,
                    new Pen(outline, 2),
                    RelativePosition,
                    Radius, Radius);
            }
            else
            {
                drawingContext.DrawEllipse(
                    background,
                    null,//new Pen(outline, 2),
                    RelativePosition,
                    Radius, Radius);
            }
        }
    }
}
