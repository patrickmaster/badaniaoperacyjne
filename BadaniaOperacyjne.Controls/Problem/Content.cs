using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class Content : ItemsControl// : Translated
    {
        #region Scale DP
        public int Scale
        {
            get { return (int)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", 
            typeof(int), 
            typeof(Content), 
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(ScaleProperty_Changed)));

        private static void ScaleProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScaleChangedEventArgs args = new ScaleChangedEventArgs
                {
                    OldScale = (int)e.OldValue,
                    NewScale = (int)e.NewValue
                };
            if (((Content)d).ScaleChanged != null)
                ((Content)d).ScaleChanged(d, args);
            ((Content)d).OnScaleChanged(d, args);
        }
        
        #endregion

        #region Postition DP

        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PositionY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            "Position", 
            typeof(Point), 
            typeof(Content), 
            new FrameworkPropertyMetadata(new Point(), Position_Changed));

        private static void Position_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PositionChangedEventArgs args = new PositionChangedEventArgs
            {
                OldPosition = (Point)e.OldValue,
                NewPosition = (Point)e.NewValue
            };
            if (((Content)d).PositionChanged != null)
                ((Content)d).PositionChanged(d, args);
            ((Content)d).OnPositionChanged(d, args);
        }

        #endregion

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
            typeof(Content), 
            new FrameworkPropertyMetadata(InputMode.Idle, new PropertyChangedCallback(InputModeProperty_Changed)));

        private static void InputModeProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((Content)d).InputModeChanged != null)
                ((Content)d).InputModeChanged(d, new ValueChangedEventArgs<InputMode>
                {
                    OldValue = (InputMode)e.OldValue,
                    NewValue = (InputMode)e.NewValue
                });
        }

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
            typeof(Content), 
            new FrameworkPropertyMetadata(5d, new PropertyChangedCallback(RadiusProperty_Changed)));

        private static void RadiusProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((Content)d).RadiusChanged != null)
            {
                ((Content)d).RadiusChanged(d, new RadiusChangedEventArgs
                {
                    OldRadius = (double)e.OldValue,
                    NewRadius = (double)e.NewValue
                });
            }
        }

        #endregion

        protected delegate void ScaleChangedEventHandler(object sender, ScaleChangedEventArgs e);
        internal delegate void PositionChangedEventHandler(object sender, PositionChangedEventArgs e);
        protected delegate void RadiusChangedEventHandler(object sender, RadiusChangedEventArgs e);
        internal delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs<InputMode> e);
        protected event ScaleChangedEventHandler ScaleChanged;
        internal event PositionChangedEventHandler PositionChanged;
        protected event RadiusChangedEventHandler RadiusChanged;
        internal event ValueChangedEventHandler InputModeChanged;

        Size size;
        public Content()
        {
            this.SizeChanged += (o, e) =>
                {
                    size = e.NewSize;
                    this.InvalidateVisual(); // cause a render
                };

            this.MouseWheel += Content_MouseWheel;
            this.MouseRightButtonDown += Content_MouseRightButtonDown;
            this.MouseRightButtonUp += Content_MouseRightButtonUp;
            this.MouseMove += Content_MouseMove;
            this.MouseLeftButtonDown += Content_MouseLeftButtonDown;
            this.RadiusChanged += Content_RadiusChanged;
            this.InputModeChanged += Content_InputModeChanged;
            this.KeyDown += Content_KeyDown;
            PlaceVisual.Dragging += PlaceVisual_Dragging;
        }

        void PlaceVisual_Dragging(object sender, DraggingEventArgs e)
        {
            InputMode = InputMode.Idle;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            Console.WriteLine("got focus");
            base.OnGotFocus(e);
        }

        void Content_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("key down");
        }

        void Content_InputModeChanged(object sender, ValueChangedEventArgs<InputMode> e)
        {
            if (e.NewValue != InputMode.Idle)
                Mouse.OverrideCursor = Cursors.Cross;
            else
                Mouse.OverrideCursor = null;
        }

        void Content_RadiusChanged(object sender, RadiusChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        void Content_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (InputMode)
            {
                case InputMode.InputPlace:
                    //points.Add(new Point(0, 0));
                    Console.WriteLine("inputting place");
                    InputPlace(e.GetPosition(this));
                    break;
                case InputMode.InputPetrolPlace:
                    InputPetrolPlace(e.GetPosition(this));
                    Console.WriteLine("inputting petrol place");
                    break;
                case InputMode.Idle:
                    Console.WriteLine("idle input mode");
                    break;
            }
        }

        private void InputPetrolPlace(Point point)
        {
            this.AddChild(new PetrolPlaceVisual(Translator.DescalePoint(Translator.GetAbsoluteCoords(point, size, Position), (double)Scale)));
            this.InvalidateVisual();
        }

        private void InputPlace(Point point)
        {
            Point absoluteDescaledCoords = Translator.DescalePoint(Translator.GetAbsoluteCoords(point, size, Position), (double)Scale);
            //Places.Add(new PackagePlace(absoluteDescaledCoords));
            this.AddChild(new PackagePlaceVisual(absoluteDescaledCoords));
            this.InvalidateVisual();
        }

        bool isDragging = false;
        Point draggingStartRelativePosition;
        Point draggingStartPosition;
        Point draggingDelta = new Point();
        void Content_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (InputMode == InputMode.Idle)
            {
                isDragging = true;
                draggingStartRelativePosition = e.GetPosition(this);
                draggingStartPosition = Position;
                Mouse.OverrideCursor = Cursors.Hand;
            }
            else
            {
                InputMode = InputMode.Idle;
            }
        }

        void Content_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point position = e.GetPosition(this);
                draggingDelta = (Point)(position - draggingStartRelativePosition);
                Position = new Point(
                    draggingStartPosition.X + draggingDelta.X,
                    draggingStartPosition.Y + draggingDelta.Y);

                //Console.WriteLine(center.X + "x" + center.Y);
                this.InvalidateVisual();
            }
        }

        void Content_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Mouse.OverrideCursor = null;
            draggingDelta.X = 0;
            draggingDelta.Y = 0;
            this.InvalidateVisual();
        }

        void Content_MouseWheel(object sender, MouseWheelEventArgs e)
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

        //List<Point> points = new List<Point>();
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

        protected virtual void OnScaleChanged(object sender, ScaleChangedEventArgs e)
        {
            Position = Translator.ScalePoint(Position, e.NewScale - e.OldScale);
            this.InvalidateVisual();
        }

        protected virtual void OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        internal IEnumerable<Place> ToPlacesCollection()
        {
            List<Place> places = new List<Place>();
            IEnumerator enumerator = this.LogicalChildren;
            while (enumerator.MoveNext())
            {
                object element = enumerator.Current;
                if (element is PetrolPlaceVisual)
                {
                    places.Add(new Place
                    {
                        Position = ((PetrolPlaceVisual)element).Position,
                        Type = PlaceType.Petrol
                    });
                }
                else if (element is PackagePlaceVisual)
                {
                    places.Add(new Place
                    {
                        Position = ((PackagePlaceVisual)element).Position,
                        Type = PlaceType.Package
                    });
                }
            }

            return places;
        }
    }

    public class PositionChangedEventArgs : EventArgs
    {
        public Point OldPosition { get; set; }
        public Point NewPosition { get; set; }
    }

    public class RadiusChangedEventArgs : EventArgs
    {
        public double OldRadius { get; set; }
        public double NewRadius { get; set; }
    }
}
