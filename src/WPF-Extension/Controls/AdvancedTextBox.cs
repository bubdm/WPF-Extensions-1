using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_Extension.Controls
{

    public class AdvancedTextBox : TextBox
    {
        #region events

        event EventHandler SelectedSuggestionChanged;

        #endregion

        #region Dependency Properties

        #region Label

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(AdvancedTextBox), new PropertyMetadata(""));


        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for LabelFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double), typeof(AdvancedTextBox), new PropertyMetadata(16d));



        public Brush LabelForegorund
        {
            get => (Brush)GetValue(LabelForegorundProperty);
            set => SetValue(LabelForegorundProperty, value);
        }

        // Using a DependencyProperty as the backing store for LabelForegorund.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelForegorundProperty =
            DependencyProperty.Register("LabelForegorund", typeof(Brush), typeof(AdvancedTextBox), new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Colors.Black)));




        public Thickness LabelPadding
        {
            get => (Thickness)GetValue(LabelPaddingProperty);
            set => SetValue(LabelPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for LabelPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelPaddingProperty =
            DependencyProperty.Register("LabelPadding", typeof(Thickness), typeof(AdvancedTextBox), new PropertyMetadata(new Thickness(0)));



        #endregion

        #region Watermark

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(AdvancedTextBox), new PropertyMetadata(""));


        public Brush WatermarkForegorund
        {
            get => (Brush)GetValue(WatermarkForegorundProperty);
            set => SetValue(WatermarkForegorundProperty, value);
        }

        // Using a DependencyProperty as the backing store for WatermarkForegorund.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkForegorundProperty =
            DependencyProperty.Register("WatermarkForegorund", typeof(Brush), typeof(AdvancedTextBox), new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Colors.Gray)));

        #endregion

        #region AssistentText



        public string AssistentText
        {
            get => (string)GetValue(AssistentTextProperty);
            set => SetValue(AssistentTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for AssistentText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssistentTextProperty =
            DependencyProperty.Register("AssistentText", typeof(string), typeof(AdvancedTextBox), new PropertyMetadata(""));



        public Thickness AssistenTextPadding
        {
            get => (Thickness)GetValue(AssistenTextPaddingProperty);
            set => SetValue(AssistenTextPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for AssistenTextPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssistenTextPaddingProperty =
            DependencyProperty.Register("AssistenTextPadding", typeof(Thickness), typeof(AdvancedTextBox), new PropertyMetadata(new Thickness(0)));



        public double AssistenTextFontSize
        {
            get => (double)GetValue(AssistenTextFontSizeProperty);
            set => SetValue(AssistenTextFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for AssistenTextFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssistenTextFontSizeProperty =
            DependencyProperty.Register("AssistenTextFontSize", typeof(double), typeof(AdvancedTextBox), new PropertyMetadata(12d));





        public Brush AssistenTextForeground
        {
            get => (Brush)GetValue(AssistenTextForegroundProperty);
            set => SetValue(AssistenTextForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for AssistenTextForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssistenTextForegroundProperty =
            DependencyProperty.Register("AssistenTextForeground", typeof(Brush), typeof(AdvancedTextBox), new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Colors.Gray)));



        #endregion

        #region Suggestion

        public IEnumerable Suggestions
        {
            get => (IEnumerable)GetValue(SuggestionsProperty);
            set => SetValue(SuggestionsProperty, value);
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuggestionsProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AdvancedTextBox), new PropertyMetadata(null));



        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedObject", typeof(object), typeof(AdvancedTextBox), new PropertyMetadata(null));

        #endregion

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AdvancedTextBox), new PropertyMetadata(new CornerRadius(0)));




        public Brush HoverColor
        {
            get => (Brush)GetValue(HoverColorProperty);
            set => SetValue(HoverColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for HoverColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverColorProperty =
            DependencyProperty.Register("HoverColor", typeof(Brush), typeof(AdvancedTextBox), new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Colors.LightBlue)));


        #endregion

        private Popup _popup;
        private ListBox _listBox;

        static AdvancedTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedTextBox), new FrameworkPropertyMetadata(typeof(AdvancedTextBox)));
        }

        public AdvancedTextBox()
        {
            Loaded += (s, e) =>
            {
                TextChanged += OnTextChanged;
                _popup = (Popup)GetTemplateChild("Popup");
                _listBox = (ListBox)GetTemplateChild("ListBox");
                _listBox.MouseUp += OnMouseClick;
                KeyUp += OnKeyUp;
            };

        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            CaretIndex = Text.Length;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Enter:
                    if (_listBox.SelectedItem == null)
                        return;

                    SelectedItem = _listBox.SelectedItem;
                    Text = SelectedItem?.ToString();
                    CaretIndex = Text.Length;
                    _listBox.SelectedItem = null;
                    HidePopup();
                    SelectedSuggestionChanged?.Invoke(this, null);

                    break;
                case Key.Up:
                    if (_listBox.SelectedIndex <= 0)
                        return;
                    _listBox.SelectedIndex--;
                    break;

                case Key.Down:
                    if (_listBox.SelectedIndex >= _listBox.Items.Count - 1)
                        return;
                    _listBox.SelectedIndex++;
                    break;
                default:
                    break;
            }


        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (_listBox.SelectedItem == null)
                return;

            SelectedItem = _listBox.SelectedItem;
            Text = SelectedItem?.ToString();
            _listBox.SelectedItem = null;
            HidePopup();
            Focus();
            SelectedSuggestionChanged?.Invoke(this, new EventArgs());
        }

        private void ShowPopup()
        {
            _popup.Visibility = Visibility.Visible;
            _popup.IsOpen = true;

        }

        private void HidePopup()
        {
            _popup.Visibility = Visibility.Collapsed;
            _popup.IsOpen = false;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Suggestions == null)
                return;

            if (string.IsNullOrEmpty(Text))
            {
                HidePopup();
                SelectedItem = null;
                return;
            }
            var items = new HashSet<object>();
            foreach (var item in Suggestions)
            {

                var name = item.ToString().ToLower();
                if (name.Contains(Text.ToLower()))
                    items.Add(item);
            }

            _listBox.ItemsSource = items;
            if (items.Count > 0)
                ShowPopup();
            else
                HidePopup();
        }


    }
}
