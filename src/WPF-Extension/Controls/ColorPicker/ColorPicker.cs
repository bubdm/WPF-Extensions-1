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
using WPF_Extension.ColorExtensions;

namespace WPF_Extension.Controls.ColorPicker
{

    public class ColorPicker : Control
    {

        #region Private fields

        private bool _updateSpectrumSlider = true;

        private ColorSlider _spectrumSlider;
        private ColorSlider _alphaSlider;
        private ColorSlider _rSlider;
        private ColorSlider _gSlider;
        private ColorSlider _bSlider;

        private ColorCanvas _colorCanvas;
        private TextBox _hexTextBox;
        private Color _oldColor = Colors.Transparent;
        private bool _updateHexColor;
        private bool _supressUpdates;
        private const string _part_SpectrumSlider = "PART_SpectrumSlider";
        private const string _part_AlphaSlider = "PART_AlphaSlider";
        private const string _part_ColorCanvas = "PART_ColorCanvas";
        private const string _part_HexTextBox = "PART_HexTextBox";
        private const string _part_RSlider = "PART_RSlider";
        private const string _part_GSlider = "PART_GSlider";
        private const string _part_BSlider = "PART_BSlider";

        #endregion

        #region Dependency Properties

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Transparent, OnSelectedColorChanged));



        public bool ShowRBGSliders
        {
            get => (bool)GetValue(ShowRBGSlidersProperty);
            set => SetValue(ShowRBGSlidersProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowRBGSliders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowRBGSlidersProperty =
            DependencyProperty.Register("ShowRBGSliders", typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));




        public bool UseAlphaChannel
        {
            get => (bool)GetValue(UseAlphaChannelProperty);
            set => SetValue(UseAlphaChannelProperty, value);
        }

        // Using a DependencyProperty as the backing store for UseAlphaChannel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseAlphaChannelProperty =
            DependencyProperty.Register("UseAlphaChannel", typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));




        #endregion

        #region Events

        #region SelectedColor

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(nameof(SelectedColorChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));
        public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
        {
            add { AddHandler(SelectedColorChangedEvent, value); }
            remove { RemoveHandler(SelectedColorChangedEvent, value); }
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = d as ColorPicker;
            if (picker != null)
                picker.OnSelectedColorChanged((Color)e.OldValue, (Color)e.NewValue);
        }

        protected virtual void OnSelectedColorChanged(Color oldValue, Color newValue)
        {
            if (_supressUpdates)
                return;

            UpdateSliders(newValue);
            UpdateHexColor(newValue);
            UpdateCanvasColor(newValue);

            var args = new RoutedPropertyChangedEventArgs<Color>(oldValue, newValue)
            {
                RoutedEvent = SelectedColorChangedEvent
            };

            RaiseEvent(args);
        }

        #endregion

        #endregion

        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
        }

        #region Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GetParts();

            if (_alphaSlider != null)
                _alphaSlider.Value = 255;


            if (_hexTextBox != null)
                _hexTextBox.LostFocus += HexTextBox_LostFocus;

            if (_colorCanvas != null)
                _colorCanvas.SelectedColorChanged += ColorCanvas_SelectedColorChanged;

            if (_alphaSlider != null)
                _alphaSlider.ValueChanged += AlphaSlider_ValueChanged;

            if (_rSlider != null)
                _rSlider.ValueChanged += RSlider_ValueChanged;

            if (_gSlider != null)
                _gSlider.ValueChanged += GSlider_ValueChanged;

            if (_bSlider != null)
                _bSlider.ValueChanged += BSlider_ValueChanged;
        }

        #endregion

        #region Event Methods

        private void HexTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!_updateHexColor)
                return;

            object color = null;
            try
            {
                color = ColorConverter.ConvertFromString(_hexTextBox.Text);
            }
            catch
            {
                return;
            }


            if (color != null)
                UpdateSelectedColor((Color)color, true);
        }

        private void ColorCanvas_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            UpdateSelectedColor(e.NewValue, false);
        }

        private void AlphaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var col = SelectedColor;
            col.A = (byte)e.NewValue;
            UpdateSelectedColor(col, false);

        }

        private void RSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var col = SelectedColor;
            col.R = (byte)e.NewValue;
            UpdateSelectedColor(col, true);
        }

        private void GSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var col = SelectedColor;
            col.G = (byte)e.NewValue;
            UpdateSelectedColor(col, true);
        }

        private void BSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var col = SelectedColor;
            col.B = (byte)e.NewValue;
            UpdateSelectedColor(col, true);
        }


        #endregion

        #region Private Methods

        private void GetParts()
        {
            _spectrumSlider = GetTemplateChild(_part_SpectrumSlider) as ColorSlider;
            _alphaSlider = GetTemplateChild(_part_AlphaSlider) as ColorSlider;
            _rSlider = GetTemplateChild(_part_RSlider) as ColorSlider;
            _gSlider = GetTemplateChild(_part_GSlider) as ColorSlider;
            _bSlider = GetTemplateChild(_part_BSlider) as ColorSlider;

            _colorCanvas = GetTemplateChild(_part_ColorCanvas) as ColorCanvas;
            _hexTextBox = GetTemplateChild(_part_HexTextBox) as TextBox;

        }

        private void UpdateSliders(Color color)
        {
            _supressUpdates = true;

            var noAColor = color;
            noAColor.A = 255;



            if (_spectrumSlider != null && _updateSpectrumSlider)
                _spectrumSlider.Value = 360 - noAColor.GetHue();


            if (_rSlider != null)
                _rSlider.Value = color.R;

            if (_gSlider != null)
                _gSlider.Value = color.G;

            if (_bSlider != null)
                _bSlider.Value = color.B;

            if (_alphaSlider != null && noAColor != _oldColor)
            {
                _oldColor = noAColor;
                var stops = new GradientStopCollection
                {
                    new GradientStop(noAColor, 0),
                    new GradientStop(Colors.Transparent,1)
                };
                _alphaSlider.GradientStops = stops;
                _alphaSlider.Value = color.A;
            }

            _supressUpdates = false;
        }

        private void UpdateHexColor(Color color)
        {
            _supressUpdates = true;
            string hexString;
            if(UseAlphaChannel)
                hexString = $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
            else
                hexString = $"#{color.R:X2}{color.G:X2}{color.B:X2}";

            if (_hexTextBox != null)
            {
                _updateHexColor = false;
                _hexTextBox.Text = hexString;
                _updateHexColor = true;
            }

            _supressUpdates = false;

        }

        private void UpdateCanvasColor(Color color)
        {
            _supressUpdates = true;
            var colA = color;
            colA.A = 255;
            _colorCanvas.SelectedColor = colA;
            _supressUpdates = false;
        }

        private void UpdateSelectedColor(Color color, bool updateSpectrum)
        {
            var col = color;
            col.A = UseAlphaChannel ? (byte)_alphaSlider.Value : (byte)255;
            _updateSpectrumSlider = updateSpectrum;
            SelectedColor = col;
            _updateSpectrumSlider = true;
        }

        #endregion
    }
}
