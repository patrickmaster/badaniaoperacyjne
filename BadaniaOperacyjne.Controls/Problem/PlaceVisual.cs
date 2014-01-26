using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BadaniaOperacyjne.Controls.Problem
{
    internal abstract class PlaceVisual : FrameworkElement
    {
        #region Position DP

        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            "Position", 
            typeof(Point), 
            typeof(PlaceVisual), 
            new FrameworkPropertyMetadata(new Point(), new PropertyChangedCallback(PositionProperty_Changed)));

        private static void PositionProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((PlaceVisual)d).PositionChanged != null)
            {
                ((PlaceVisual)d).PositionChanged(d, new ValueChangedEventArgs<Point>
                {
                    OldValue = (Point)e.OldValue,
                    NewValue = (Point)e.NewValue
                });
            }
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
            typeof(PlaceVisual), 
            new PropertyMetadata(5d));

        #endregion

        protected Point RelativePosition
        {
            get
            {
                if (ContentParent != null)
                {
                    Point position = Translator.GetRelativeCoords(
                        Translator.ScalePoint(Position, (double)ContentParent.Scale),
                        ContentParent.RenderSize,
                        ContentParent.Position);
                    return position;
                }
                else
                    return new Point();
            }
            private set { }
        }

        protected Content ContentParent
        {
            get
            {
                Content parent = null;
                try
                {
                    parent = (Content)this.Parent;
                }
                catch { }
                return parent;

            }
            private set  {}
        }

        internal delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs<Point> e);
        internal event ValueChangedEventHandler PositionChanged;
        internal delegate void DragStartEventHandler(object sender, DragStartEventArgs e);
        internal static event DragStartEventHandler DragStart;
        internal delegate void DraggingEventHandler(object sender, DraggingEventArgs e);
        internal static event DraggingEventHandler Dragging;
        internal delegate void DragEndEventHandler(object sender, DragEndEventArgs e);
        internal static event DragEndEventHandler DragEnd;
        internal bool Selected { get; set; }

        static PlaceVisual()
        {
            FocusVisualStyleProperty.OverrideMetadata(
                typeof(PlaceVisual),
                new FrameworkPropertyMetadata(new Style() { TargetType = typeof(Control) }));
        }

        public PlaceVisual()
        {
            this.MouseLeftButtonDown += Place_MouseLeftButtonDown;
            DragStart += PlaceVisual_DragStart;
            Dragging += PlaceVisual_Dragging;
        }

        Point draggingStartPosition;
        Point draggingDelta = new Point();

        void PlaceVisual_DragStart(object sender, DragStartEventArgs e)
        {
            //draggingStartRelativePosition = RelativePosition;
            draggingStartPosition = Position;
        }

        void PlaceVisual_Dragging(object sender, DraggingEventArgs e)
        {
            if (Selected)
            {
                Position = new Point(
                    draggingStartPosition.X + e.Delta.X,
                    draggingStartPosition.Y + e.Delta.Y);
                this.InvalidateVisual();
            }
        }

        public PlaceVisual(Point position)
            : this()
        {
            Position = position;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ContentParent.PositionChanged += ContentParent_PositionChanged;
            ContentParent.MouseMove += ContentParent_MouseMove;
            ContentParent.MouseLeftButtonUp += ContentParent_MouseLeftButtonUp;
            ContentParent.MouseDown += ContentParent_MouseDown;
            ContentParent.KeyDown += ContentParent_KeyDown;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            Console.WriteLine("got focus on place ");
            base.OnGotFocus(e);
        }

        void ContentParent_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("delete pressed");
        }

        void ContentParent_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        bool isDragging = false;
        Point draggingStartRelativePosition;
        void Place_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Selected = true;
            isDragging = true;
            draggingStartRelativePosition = e.GetPosition(ContentParent);
            //draggingStartPosition = Position;
            if (PlaceVisual.DragStart != null)
                PlaceVisual.DragStart(this, new DragStartEventArgs());
        }

        void ContentParent_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point position = e.GetPosition(this);
                draggingDelta = Translator.DescalePoint((Point)(position - draggingStartRelativePosition), (double)ContentParent.Scale);
                //Position = new Point(
                //    draggingStartPosition.X + draggingDelta.X,
                //    draggingStartPosition.Y + draggingDelta.Y);
                if (Dragging != null)
                {
                    Dragging(this, new DraggingEventArgs
                    {
                        //StartingPoint = draggingStartPosition,
                        Delta = draggingDelta
                    });
                }
                //Console.WriteLine(center.X + "x" + center.Y);
            }
        }

        void ContentParent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            if (PlaceVisual.DragEnd != null)
                PlaceVisual.DragEnd(this, new DragEndEventArgs());
            Mouse.OverrideCursor = null;
            draggingDelta.X = 0;
            draggingDelta.Y = 0;
            this.InvalidateVisual();
        }

        void ContentParent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Selected = false;
        }
    }

    internal class DraggingEventArgs : EventArgs
    {
        //public Point StartingPoint { get; set; }
        public Point Delta { get; set; }
    }

    internal class DragStartEventArgs : EventArgs
    {
        //public Point StartingPoint { get; set; }
    }

    internal class DragEndEventArgs : EventArgs
    {
        //public Point EndingPoint { get; set; }
    }
}
