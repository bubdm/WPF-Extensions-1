using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_Extension.Controls
{
    public class ColorPresenter : Control
    {

        #region Dependency Properties

        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        // Using a DependencyProperty as the backing store for ColorProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(ColorPresenter), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));


        #endregion

        static ColorPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPresenter), new FrameworkPropertyMetadata(typeof(ColorPresenter)));
        }


    }
}
