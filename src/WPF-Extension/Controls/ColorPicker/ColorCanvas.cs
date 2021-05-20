using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Extension.ColorExtensions;

namespace WPF_Extension.Controls.ColorPicker
{
    public class ColorCanvas : Control
    {
        private readonly TranslateTransform _selectorTransform = new();
        private Point _currentColorPos;

        #region Dependency Properties

        public Color CanvasColor
        {
            get => (Color)GetValue(CanvasColorProperty);
            set => SetValue(CanvasColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for MainColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanvasColorProperty =
            DependencyProperty.Register(nameof(CanvasColor), typeof(Color), typeof(ColorCanvas), new PropertyMetadata(Colors.White, OnCanvasColorChanged));
        private bool _raiseSelectedColorChanged;

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorCanvas), new PropertyMetadata(Colors.White, OnSelectedColorChanged));



        public FrameworkElement Selector
        {
            get => (FrameworkElement)GetValue(SelectorProperty);
            set => SetValue(SelectorProperty, value);
        }

        // Using a DependencyProperty as the backing store for Selector.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectorProperty =
            DependencyProperty.Register("Selector", typeof(Shape), typeof(ColorCanvas), new PropertyMetadata(null));

        #endregion

        #region Events

        #region SelectedColor

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(nameof(SelectedColorChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorCanvas));
        public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
        {
            add { AddHandler(SelectedColorChangedEvent, value); }
            remove { RemoveHandler(SelectedColorChangedEvent, value); }
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = d as ColorCanvas;
            if (canvas != null)
                canvas.OnSelectedColorChanged((Color)e.OldValue, (Color)e.NewValue);
        }

        protected virtual void OnSelectedColorChanged(Color oldValue, Color newValue)
        {
            if (!_raiseSelectedColorChanged)
                return;

            UpdateSelectorPosition(newValue);

            var args = new RoutedPropertyChangedEventArgs<Color>(oldValue, newValue)
            {
                RoutedEvent = SelectedColorChangedEvent
            };

            RaiseEvent(args);
        }

        #endregion

        #region CanvasColor

        public static readonly RoutedEvent CanvasColorChangedEvent = EventManager.RegisterRoutedEvent(nameof(CanvasColorChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorCanvas));
        public event RoutedPropertyChangedEventHandler<Color> CanvasColorChanged
        {
            add { AddHandler(CanvasColorChangedEvent, value); }
            remove { RemoveHandler(CanvasColorChangedEvent, value); }
        }

        private static void OnCanvasColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var canvas = d as ColorCanvas;
            if (canvas != null)
                canvas.OnCanvasColorChanged((Color)e.OldValue, (Color)e.NewValue);
        }

        protected virtual void OnCanvasColorChanged(Color oldValue, Color newValue)
        {
            UpdateSelectedColor(_currentColorPos);
            var args = new RoutedPropertyChangedEventArgs<Color>(oldValue, newValue)
            {
                RoutedEvent = CanvasColorChangedEvent
            };

            RaiseEvent(args);
        }

        #endregion

        #endregion

        #region Ctor

        static ColorCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorCanvas), new FrameworkPropertyMetadata(typeof(ColorCanvas)));
        }

        public ColorCanvas()
        {
            Loaded += ColorCanvas_Loaded;
            SizeChanged += ColorCanvas_SizeChanged;
        }

        #endregion

        #region Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Selector == null)
            {
                Selector = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Stroke = new SolidColorBrush(Colors.White),
                    StrokeThickness = 2
                };
            }



            Selector.RenderTransform = _selectorTransform;
            MouseMove += ColorCanvas_MouseMove;
            MouseLeftButtonDown += ColorCanvas_MouseLeftButtonDown;
            MouseLeftButtonUp += ColorCanvas_MouseLeftButtonUp;
        }

        #endregion

        #region private Methods

        private Color GetColor(Point point)
        {

            NormalizePointToCanvas(ref point);
            var x = point.X / ActualWidth;
            var y = point.Y / ActualHeight;

            var hsv = new HsvColor(CanvasColor.ToHSVColor().H, x, 1 - y);

            return hsv.ToRGB();
        }

        private void NormalizePointToCanvas(ref Point point)
        {
            if (point.Y < 0)
                point.Y = 0;

            if (point.X < 0)
                point.X = 0;

            if (point.Y > ActualHeight)
                point.Y = ActualHeight;

            if (point.X > ActualWidth)
                point.X = ActualWidth;
        }

        private void UpdateSelectorPosition(Color color)
        {
            var hsv = color.ToHSVColor();


            var xCoord = hsv.S * ActualWidth;
            var yCoord = (1 - hsv.V) * ActualHeight;

            UpdateSelectorPosition(new Point(xCoord, yCoord));
        }

        private void UpdateSelectorPosition(Point point)
        {
            if (Selector == null)
                return;

            NormalizePointToCanvas(ref point);
            _currentColorPos = point;

            _selectorTransform.X = point.X - Selector.ActualWidth / 2;
            _selectorTransform.Y = point.Y - Selector.ActualHeight / 2;
        }


        private void UpdateSelectedColor(Point point)
        {
            _raiseSelectedColorChanged = false;
            SelectedColor = GetColor(point);
            _raiseSelectedColorChanged = true;
        }

        #endregion

        #region Event Methods

        private void ColorCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var xfact = e.NewSize.Width / e.PreviousSize.Width;
            var yfact = e.NewSize.Height / e.PreviousSize.Height;
            var point = new Point(_currentColorPos.X * xfact, _currentColorPos.Y * yfact);
            UpdateSelectorPosition(point);
        }

        private void ColorCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSelectorPosition(SelectedColor);
        }

        private void ColorCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }

        private void ColorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();

            var point = e.GetPosition(this);
            UpdateSelectorPosition(point);
            UpdateSelectedColor(point);
        }

        private void ColorCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            var point = e.GetPosition(this);
            UpdateSelectorPosition(point);
            UpdateSelectedColor(point);
        }

        #endregion

    }
}
