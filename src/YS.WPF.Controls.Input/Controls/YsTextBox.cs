using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YS.WPF.Controls.Input
{

    public class YsTextBox : TextBox
    {

        #region Dependency Properties

        #region Label

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(YsTextBox), new PropertyMetadata(""));


        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for LabelFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double), typeof(YsTextBox), new PropertyMetadata(16d));



        public Brush LabelForegorund
        {
            get => (Brush)GetValue(LabelForegorundProperty);
            set => SetValue(LabelForegorundProperty, value);
        }

        // Using a DependencyProperty as the backing store for LabelForegorund.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelForegorundProperty =
            DependencyProperty.Register("LabelForegorund", typeof(Brush), typeof(YsTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));




        public Thickness LabelPadding
        {
            get => (Thickness)GetValue(LabelPaddingProperty);
            set => SetValue(LabelPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for LabelPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelPaddingProperty =
            DependencyProperty.Register("LabelPadding", typeof(Thickness), typeof(YsTextBox), new PropertyMetadata(new Thickness(0)));



        #endregion

        #region Watermark

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(YsTextBox), new PropertyMetadata(""));


        public Brush WatermarkForegorund
        {
            get => (Brush)GetValue(WatermarkForegorundProperty);
            set => SetValue(WatermarkForegorundProperty, value);
        }

        // Using a DependencyProperty as the backing store for WatermarkForegorund.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkForegorundProperty =
            DependencyProperty.Register("WatermarkForegorund", typeof(Brush), typeof(YsTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        #endregion

        #region AssistentText



        public string AssistentText
        {
            get => (string)GetValue(AssistentTextProperty);
            set => SetValue(AssistentTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for AssistentText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssistentTextProperty =
            DependencyProperty.Register("AssistentText", typeof(string), typeof(YsTextBox), new PropertyMetadata(""));



        public Thickness AssistenTextPadding
        {
            get => (Thickness)GetValue(AssistenTextPaddingProperty);
            set => SetValue(AssistenTextPaddingProperty, value);
        }

        // Using a DependencyProperty as the backing store for AssistenTextPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssistenTextPaddingProperty =
            DependencyProperty.Register("AssistenTextPadding", typeof(Thickness), typeof(YsTextBox), new PropertyMetadata(new Thickness(0)));



        public double AssistenTextFontSize
        {
            get => (double)GetValue(AssistenTextFontSizeProperty);
            set => SetValue(AssistenTextFontSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for AssistenTextFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssistenTextFontSizeProperty =
            DependencyProperty.Register("AssistenTextFontSize", typeof(double), typeof(YsTextBox), new PropertyMetadata(12d));





        public Brush AssistenTextForeground
        {
            get => (Brush)GetValue(AssistenTextForegroundProperty);
            set => SetValue(AssistenTextForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for AssistenTextForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AssistenTextForegroundProperty =
            DependencyProperty.Register("AssistenTextForeground", typeof(Brush), typeof(YsTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));



        #endregion


        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(YsTextBox), new PropertyMetadata(new CornerRadius(0)));




        public Brush HoverColor
        {
            get => (Brush)GetValue(HoverColorProperty);
            set => SetValue(HoverColorProperty, value);
        }

        // Using a DependencyProperty as the backing store for HoverColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverColorProperty =
            DependencyProperty.Register("HoverColor", typeof(Brush), typeof(YsTextBox), new PropertyMetadata(new SolidColorBrush(Colors.LightBlue)));


        #endregion

        static YsTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YsTextBox), new FrameworkPropertyMetadata(typeof(YsTextBox)));
        }
    }
}
