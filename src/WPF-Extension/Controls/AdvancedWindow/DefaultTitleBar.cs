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
    public class DefaultTitleBar : Control
    {



        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DefaultTitleBar), new PropertyMetadata());


        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(DefaultTitleBar), new PropertyMetadata());


        static DefaultTitleBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DefaultTitleBar), new FrameworkPropertyMetadata(typeof(DefaultTitleBar)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var parentWindow = GetParentWindow();
            var icon = GetTemplateChild("Icon") as Image;

            icon.MouseLeftButtonDown += (s, e) => SystemCommands.ShowSystemMenu(parentWindow,PointToScreen(e.GetPosition(parentWindow)));
        }

        private Window GetParentWindow()
        {
            var window = Window.GetWindow(this);

            if (window is not null)
            {
                return window;
            }

            var parent = VisualTreeHelper.GetParent(this);
            Window parentWindow = null;

            while (parent is not null
                && (parentWindow = parent as Window) is null)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parentWindow;
        }
    }
}
