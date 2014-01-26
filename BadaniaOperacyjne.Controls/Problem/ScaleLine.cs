using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace BadaniaOperacyjne.Controls.Problem
{
    public class ScaleLine : FrameworkElement
    {
        #region Scale DP

        public int Scale
        {
            get { return (int)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", 
            typeof(int), 
            typeof(ScaleLine), 
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(ScaleProperty_Changed)));

        private static void ScaleProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((ScaleLine)d).ScaleChanged != null)
                ((ScaleLine)d).ScaleChanged(d, new ScaleChangedEventArgs
                {
                    OldScale = (int)e.OldValue,
                    NewScale = (int)e.NewValue
                });
        }

#endregion

        #region BaseDistance DP
        public double BaseDistance
        {
            get { return (double)GetValue(BaseDistanceProperty); }
            set { SetValue(BaseDistanceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Length.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BaseDistanceProperty = DependencyProperty.Register(
            "BaseDistance", 
            typeof(double), 
            typeof(ScaleLine), 
            new FrameworkPropertyMetadata(0d, new PropertyChangedCallback(BaseDistanceProperty_Changed)));

        private static void BaseDistanceProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((ScaleLine)d).BaseDistanceChanged != null)
                ((ScaleLine)d).BaseDistanceChanged(d, new BaseDistanceChangedEventArgs
                {
                    OldLength = (double)e.OldValue,
                    NewLength = (double)e.NewValue
                });
        }
        
        #endregion

        #region Thickness DP

        public int Thickness
        {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Thickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
            "Thickness", 
            typeof(int), 
            typeof(ScaleLine), 
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(ThicknessProperty_Changed)));

        private static void ThicknessProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((ScaleLine)d).ThicknessChanged != null)
                ((ScaleLine)d).ThicknessChanged(d, new ThicknessChangedEventArgs
                {
                    OldThickness = (int)e.OldValue,
                    NewThickness = (int)e.NewValue
                });
        }

        #endregion

        protected delegate void ScaleChangedEventHandler(object sender, ScaleChangedEventArgs e);
        protected event ScaleChangedEventHandler ScaleChanged;
        protected delegate void BaseDistanceChangedEventHandler(object sender, BaseDistanceChangedEventArgs e);
        protected event BaseDistanceChangedEventHandler BaseDistanceChanged;
        protected delegate void ThicknessChangedEventHandler(object sender, ThicknessChangedEventArgs e);
        protected event ThicknessChangedEventHandler ThicknessChanged;

        public ScaleLine()
        {
            ScaleChanged += ScaleLine_ScaleChanged;
        }

        void ScaleLine_ScaleChanged(object sender, ScaleChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawLine(
                new Pen(Brushes.Black, Thickness),
                new Point(0, 0),
                new Point(0, this.Height));
            double ending = this.Width * Math.Exp(Scale / 10d);
            drawingContext.DrawLine(
                new Pen(Brushes.Black, Thickness),
                new Point(ending, 0),
                new Point(ending, this.Height));
            double halfHeight = (this.Height - (double)Thickness) / 2d;
            drawingContext.DrawLine(
                new Pen(Brushes.Black, Thickness),
                new Point(0, halfHeight),
                new Point(ending, halfHeight));
            FormattedText text = new FormattedText(
                BaseDistance.ToString(), 
                CultureInfo.InvariantCulture, 
                FlowDirection.LeftToRight, 
                new Typeface("Segoe UI"),
                this.Height, 
                Brushes.Black);
           drawingContext.DrawText(text, new Point(ending + 5, 0));
        }

        //double CalculateRenderLength(
    }

    public class BaseDistanceChangedEventArgs : EventArgs
    {
        public double OldLength { get; set; }
        public double NewLength { get; set; }
    }

    public class ThicknessChangedEventArgs : EventArgs
    {
        public int OldThickness { get; set; }
        public int NewThickness { get; set; }
    }
}
