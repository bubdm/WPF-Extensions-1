using System;
using System.CodeDom;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF_Extension.ColorExtensions;

namespace WPF_Extension.Controls.ColorPicker
{
    [TemplatePart(Name = "PART_Border", Type = typeof(Border))]
    [TemplatePart(Name = "PART_Thumb", Type = typeof(Thumb))]
    public class ColorSlider : Slider
    {
        private const string _part_Border = "PART_Border";
        private const string _part_Thumb = "PART_Thumb";

        #region DependencyProperties


        public LinearGradientBrush LinearGradient
        {
            get => (LinearGradientBrush)GetValue(LinearGradientProperty);
            protected set => SetValue(_linearGradientPropertyKey, value);
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey _linearGradientPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(LinearGradient), typeof(LinearGradientBrush), typeof(ColorSlider), new PropertyMetadata(new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Red, 0),
                    new GradientStop(Colors.Yellow, 0.167),
                    new GradientStop(Colors.Lime,0.333),
                    new GradientStop(Colors.Cyan, 0.5),
                    new GradientStop(Colors.Blue,0.667),
                    new GradientStop(Colors.Magenta, 0.833),
                    new GradientStop(Colors.Red, 1)
                }
            }));

        public static readonly DependencyProperty LinearGradientProperty
            = _linearGradientPropertyKey.DependencyProperty;

        public GradientStopCollection GradientStops
        {
            get => (GradientStopCollection)GetValue(GradientStopsProperty);
            set => SetValue(GradientStopsProperty, value);
        }

        // Using a DependencyProperty as the backing store for GradientBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GradientStopsProperty =
            DependencyProperty.Register("GradientBrush", typeof(GradientStopCollection), typeof(ColorSlider), new PropertyMetadata(new GradientStopCollection
            {
                new GradientStop(Colors.Red, 0),
                new GradientStop(Colors.Yellow, 0.167),
                new GradientStop(Colors.Lime,0.333),
                new GradientStop(Colors.Cyan, 0.5),
                new GradientStop(Colors.Blue,0.667),
                new GradientStop(Colors.Magenta, 0.833),
                new GradientStop(Colors.Red, 1)
            }, OnGradientStopsChanged));

        private static void OnGradientStopsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = d as ColorSlider;
            if (slider != null)
                slider.OnGradientStopsChanged();
        }

        private void OnGradientStopsChanged()
        {
            UpdateLinearGradient(Orientation);
            UpdateColor(Value);
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            protected set => SetValue(_selectedColorPropertyKey, value);
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey _selectedColorPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(SelectedColor), typeof(Color), typeof(ColorSlider), new PropertyMetadata(Colors.Red, OnSelectedColorChanged));

        public static readonly DependencyProperty SelectedColorProperty
            = _selectedColorPropertyKey.DependencyProperty;

        #endregion

        #region Events

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(nameof(SelectedColorChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorSlider));
        public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
        {
            add { AddHandler(SelectedColorChangedEvent, value); }
            remove { RemoveHandler(SelectedColorChangedEvent, value); }
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = d as ColorSlider;
            if (slider != null)
                slider.OnSelectedColorChanged((Color)e.OldValue, (Color)e.NewValue);
        }

        protected virtual void OnSelectedColorChanged(Color oldValue, Color newValue)
        {
            var args = new RoutedPropertyChangedEventArgs<Color>(oldValue, newValue)
            {
                RoutedEvent = SelectedColorChangedEvent
            };

            RaiseEvent(args);
        }

        #endregion

        static ColorSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSlider), new FrameworkPropertyMetadata(typeof(ColorSlider)));
            OrientationProperty.OverrideMetadata(typeof(ColorSlider), new FrameworkPropertyMetadata(Orientation.Vertical, OnOrientationChanged));
        }

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slider = d as ColorSlider;
            if (slider != null)
                slider.OnOrientationChanged((Orientation)e.OldValue, (Orientation)e.NewValue);
        }

        private void OnOrientationChanged(Orientation oldValue, Orientation newValue)
        {
            UpdateLinearGradient(newValue);
            AdjustBorderAndThumb();
        }

        public ColorSlider()
        {
            Loaded += ColorSlider_Loaded;
            SizeChanged += (s, e) => AdjustBorderAndThumb();
        }

        private void ColorSlider_Loaded(object sender, RoutedEventArgs e)
        {
            AdjustBorderAndThumb();
        }

        private void AdjustBorderAndThumb()
        {
            var border = GetTemplateChild(_part_Border) as Border;
            var thumb = GetTemplateChild(_part_Thumb) as Thumb;

            if (border == null || thumb == null)
                return;
            thumb.Width = Math.Min(ActualHeight, ActualWidth);
            thumb.Height = thumb.Width;
            border.Margin = Orientation == Orientation.Vertical ? new Thickness(0, thumb.Width / 2, 0, thumb.Width / 2) : new Thickness(thumb.Width / 2, 0, thumb.Width / 2, 0);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            OnValueChanged(0, Value);
            OnOrientationChanged(Orientation, Orientation);
            UpdateColor(Value);
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            UpdateColor(newValue);
        }

        private void UpdateColor(double value)
        {
            if (LinearGradient == null)
                return;
            var diff = Maximum - Minimum;
            SelectedColor = Orientation == Orientation.Vertical ? LinearGradient.GetColorAtOffset(1 - value / diff) : LinearGradient.GetColorAtOffset(value / diff);
        }

        private void UpdateLinearGradient(Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                var newbrush = new LinearGradientBrush
                {
                    GradientStops = GradientStops,
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 0)
                };
                LinearGradient = newbrush;
            }
            else
            {
                var newbrush = new LinearGradientBrush
                {
                    GradientStops = GradientStops,
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(0, 1)
                };
                LinearGradient = newbrush;
            }
        }
    }
}
