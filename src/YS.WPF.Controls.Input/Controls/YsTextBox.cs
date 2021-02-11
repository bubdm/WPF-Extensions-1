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
            DependencyProperty.Register("LabelFontSize", typeof(double), typeof(YsTextBox), new PropertyMetadata(10d));



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


        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(YsTextBox), new PropertyMetadata(new CornerRadius(0)));

       #endregion

        static YsTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YsTextBox), new FrameworkPropertyMetadata(typeof(YsTextBox)));
        }
        public YsTextBox()
        {
            
        }
    }
}
