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
        protected const double SIZE = 10;
        //protected const double THICKNESS = 1;
        //protected const int SCALE_FACTOR = 1;

        protected delegate void ScaleChangedEventHandler(object sender, ScaleChangedEventArgs e);
        protected event ScaleChangedEventHandler ScaleChanged;
        protected delegate void ThicknessChangedEventHandler(object sender, ThicknessChangedEventArgs e);
        protected event ThicknessChangedEventHandler ThicknessChanged;
        protected delegate void TranslationChangedEventHandler(object sender, TranslationChangedEventArgs e);
        protected event TranslationChangedEventHandler TranslationChanged;
        protected delegate void ScaleLineLengthChangedEventHandler(object sender, ValueChangedEventArgs<double> e);
        protected event ScaleLineLengthChangedEventHandler ScaleLineLengthChanged;

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
            typeof(Axis), 
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(Scale_Changed)));

        private static void Scale_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScaleChangedEventArgs args = new ScaleChangedEventArgs
            {
                OldScale = (int)e.OldValue,
                NewScale = (int)e.NewValue
            };
            
            if (((Axis)d).ScaleChanged != null)
                ((Axis)d).ScaleChanged(d, args);
            ((Axis)d).OnScaleChanged(d, args);
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
            typeof(Axis), 
            new FrameworkPropertyMetadata(1, new PropertyChangedCallback(ThicknessProperty_Changed)));

        private static void ThicknessProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        
        #region Translation DP

        public double Translation
        {
            get { return (double)GetValue(TranslationProperty); }
            set { SetValue(TranslationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TranslationProperty = DependencyProperty.Register(
            "Translation", 
            typeof(double), 
            typeof(Axis), 
            new FrameworkPropertyMetadata(0d, new PropertyChangedCallback(TranslationProperty_Changed)));

        private static void TranslationProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((Axis)d).TranslationChanged != null)
                ((Axis)d).TranslationChanged(d, new TranslationChangedEventArgs
                {
                    OldTranslation = (double)e.OldValue,
                    NewTranslation = (double)e.NewValue
                });
                    
        }

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
            typeof(Axis), 
            new FrameworkPropertyMetadata(10d, new PropertyChangedCallback(ScaleLineLengthProperty_Changed)));

        private static void ScaleLineLengthProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((Axis)d).ScaleLineLengthChanged != null)
            {
                ((Axis)d).ScaleLineLengthChanged(d, new ValueChangedEventArgs<double>
                {
                    OldValue = (double)e.OldValue,
                    NewValue = (double)e.NewValue
                });
            }
        }

        #endregion

        public Axis()
        {
            this.SizeChanged += Axis_SizeChanged;
            this.MouseWheel += Axis_MouseWheel;
            this.TranslationChanged += Axis_TranslationChanged;
        }

        void Axis_TranslationChanged(object sender, TranslationChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        void Axis_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Scale++;
            }
            else if (e.Delta < 0)
            {
                Scale--;
            }
        }

        void Axis_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            size = e.NewSize;

            this.InvalidateVisual();
        }

        protected virtual void OnScaleChanged(object sender, ScaleChangedEventArgs e)
        {
            this.InvalidateVisual(); 
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(
                Brushes.Transparent,
                new Pen(),
                new Rect(
                    new Point(0, 0),
                    new Point(size.Width - 1, size.Height - 1)));
        }

        protected double CalculateCenter(double size)
        {
            return size / 2d + Translation;
        }

        protected IEnumerable<double> CalculateScaleLines(double size, double stepFraction)
        {
            throw new NotImplementedException();
        }
    }

    public class ScaleChangedEventArgs : EventArgs
    {
        public int OldScale { get; set; }
        public int NewScale { get; set; }
    }

    public class TranslationChangedEventArgs : EventArgs
    {
        public double OldTranslation { get; set; }
        public double NewTranslation { get; set; }
    }

    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T OldValue { get; set; }
        public T NewValue { get; set; }
    }
}
