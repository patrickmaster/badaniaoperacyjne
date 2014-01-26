using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace BadaniaOperacyjne.Controls.Problem
{
    internal class PetrolPlaceVisual : PlaceVisual
    {
        protected static Brush outline = Brushes.Crimson;
        protected static Brush background = Brushes.DarkBlue;

        public PetrolPlaceVisual(Point position)
            : base(position)
        {
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
