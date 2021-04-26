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
using ControlzEx.Behaviors;
using Microsoft.Xaml.Behaviors;

namespace WPF_Extensions.Controls
{
    
    public class AdvancedWindow : Window
    {

        #region DependencyProperties



        public Thickness ResizeBoarderThickness
        {
            get => (Thickness)GetValue(ResizeBoarderThicknessProperty);
            set => SetValue(ResizeBoarderThicknessProperty, value);
        }

        // Using a DependencyProperty as the backing store for ResizeBoarderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResizeBoarderThicknessProperty =
            DependencyProperty.Register(nameof(ResizeBoarderThickness), typeof(Thickness), typeof(AdvancedWindow), new PropertyMetadata(WindowChromeBehavior.ResizeBorderThicknessProperty.DefaultMetadata.DefaultValue));


        public bool OverlapTaskbarOnMaximize
        {
            get => (bool)GetValue(OverlapTaskbarOnMaximizeProperty);
            set => SetValue(OverlapTaskbarOnMaximizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for OverlapTaskbarOnMaximize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OverlapTaskbarOnMaximizeProperty =
            DependencyProperty.Register(nameof(OverlapTaskbarOnMaximize), typeof(bool), typeof(AdvancedWindow), new PropertyMetadata(false));


        public bool KeepBorderOnMaximize
        {
            get => (bool)GetValue(KeepBorderOnMaximizeProperty);
            set => SetValue(KeepBorderOnMaximizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for KeepBorderOnMaximize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeepBorderOnMaximizeProperty =
            DependencyProperty.Register(nameof(KeepBorderOnMaximize), typeof(bool), typeof(AdvancedWindow), new PropertyMetadata(false));

        

        public bool TryToBeFlickerFree
        {
            get => (bool)GetValue(TryToBeFlickerFreeProperty);
            set => SetValue(TryToBeFlickerFreeProperty, value);
        }

        // Using a DependencyProperty as the backing store for TryToBeFlickerFree.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TryToBeFlickerFreeProperty =
            DependencyProperty.Register(nameof(TryToBeFlickerFree), typeof(bool), typeof(AdvancedWindow), new PropertyMetadata(true));




        public Brush WindowGlow
        {
            get => (Brush)GetValue(WindowGlowProperty);
            set => SetValue(WindowGlowProperty, value);
        }


        // Using a DependencyProperty as the backing store for WindowGlow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowGlowProperty =
            DependencyProperty.Register("WindowGlow", typeof(Brush), typeof(AdvancedWindow), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007ACC"))));




        public Brush NonActiveWindowGlow
        {
            get => (Brush)GetValue(NonActiveWindowGlowProperty);
            set => SetValue(NonActiveWindowGlowProperty, value);
        }

        // Using a DependencyProperty as the backing store for NonActiveWindowGlow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NonActiveWindowGlowProperty =
            DependencyProperty.Register("NonActiveWindowGlow", typeof(Brush), typeof(AdvancedWindow), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3E3E42"))));   



        public bool ShowMinButton
        {
            get => (bool)GetValue(ShowMinButtonProperty);
            set => SetValue(ShowMinButtonProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowMinButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowMinButtonProperty =
            DependencyProperty.Register("ShowMinButton", typeof(bool), typeof(AdvancedWindow), new PropertyMetadata(true));



        public bool ShowMaxRestoreButton
        {
            get => (bool)GetValue(ShowMaxRestoreButtonProperty);
            set => SetValue(ShowMaxRestoreButtonProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowMaxRestoreButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowMaxRestoreButtonProperty =
            DependencyProperty.Register("ShowMaxRestoreButton", typeof(bool), typeof(AdvancedWindow), new PropertyMetadata(true));




        #endregion

        static AdvancedWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(typeof(AdvancedWindow)));

            BorderThicknessProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(new Thickness(1)));
            WindowStyleProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(WindowStyle.None));
            AllowsTransparencyProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(false));
        }

        public AdvancedWindow()
        {
            InitializeWindowChrome();
        }

        private void InitializeWindowChrome()
        {
            var behavior = new WindowChromeBehavior();

            BindingOperations.SetBinding(behavior, WindowChromeBehavior.ResizeBorderThicknessProperty, new Binding { Path = new PropertyPath(ResizeBoarderThicknessProperty), Source = this });
            BindingOperations.SetBinding(behavior, WindowChromeBehavior.IgnoreTaskbarOnMaximizeProperty, new Binding { Path = new PropertyPath(OverlapTaskbarOnMaximizeProperty), Source = this });
            BindingOperations.SetBinding(behavior, WindowChromeBehavior.TryToBeFlickerFreeProperty, new Binding { Path = new PropertyPath(TryToBeFlickerFreeProperty), Source = this });
            BindingOperations.SetBinding(behavior, WindowChromeBehavior.EnableMinimizeProperty, new Binding { Path = new PropertyPath(ShowMinButtonProperty), Source = this });
            BindingOperations.SetBinding(behavior, WindowChromeBehavior.EnableMaxRestoreProperty, new Binding { Path = new PropertyPath(ShowMaxRestoreButtonProperty), Source = this });

            Interaction.GetBehaviors(this).Add(behavior);
        }
    }
}
