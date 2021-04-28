using System;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ControlzEx.Behaviors;
using ControlzEx.Native;
using ControlzEx.Standard;
using Microsoft.Xaml.Behaviors;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace WPF_Extension.Controls
{
    [TemplatePart(Name = "PART_CommandButtons", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_TitleBar", Type = typeof(UIElement))]
    public class AdvancedWindow : Window
    {

        private static readonly PropertyInfo _criticalHandlePropertyInfo = typeof(Window).GetProperty("CriticalHandle", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly object[] _emptyObjectArray = Array.Empty<object>();

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
            DependencyProperty.Register("WindowGlow", typeof(Brush), typeof(AdvancedWindow), new PropertyMetadata());




        public Brush NonActiveWindowGlow
        {
            get => (Brush)GetValue(NonActiveWindowGlowProperty);
            set => SetValue(NonActiveWindowGlowProperty, value);
        }

        // Using a DependencyProperty as the backing store for NonActiveWindowGlow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NonActiveWindowGlowProperty =
            DependencyProperty.Register("NonActiveWindowGlow", typeof(Brush), typeof(AdvancedWindow), new PropertyMetadata());



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


        public UIElement TitleBar
        {
            get => (UIElement)GetValue(TitleBarProperty);
            set => SetValue(TitleBarProperty, value);
        }

        // Using a DependencyProperty as the backing store for TitleBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleBarProperty =
            DependencyProperty.Register("TitleBar", typeof(UIElement), typeof(AdvancedWindow), new PropertyMetadata());


        public UIElement CommandButtons
        {
            get => (UIElement)GetValue(CommandButtonsProperty);
            set => SetValue(CommandButtonsProperty, value);
        }

        // Using a DependencyProperty as the backing store for CommandButtons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandButtonsProperty =
            DependencyProperty.Register("CommandButtons", typeof(UIElement), typeof(AdvancedWindow), new PropertyMetadata());

        #region TitleBar



        public Brush TitleBarBackground
        {
            get => (Brush)GetValue(TitleBarBackgroundProperty);
            set => SetValue(TitleBarBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for TitleBarBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleBarBackgroundProperty =
            DependencyProperty.Register("TitleBarBackground", typeof(Brush), typeof(AdvancedWindow), new PropertyMetadata(null));



        #endregion

        #endregion

        static AdvancedWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(typeof(AdvancedWindow)));

            BorderThicknessProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(new Thickness(1)));
            WindowStyleProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(WindowStyle.None));
            AllowsTransparencyProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(false));
            BackgroundProperty.OverrideMetadata(typeof(AdvancedWindow), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White)));
        }

        public AdvancedWindow()
        {
            ContentRendered += OnContentRendered;

            InitializeWindowChrome();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            
            if(TitleBarBackground == null)
            {
                BindingOperations.SetBinding(this, TitleBarBackgroundProperty, new Binding { Path = new PropertyPath(TitleBarBackgroundProperty), Source = this });
            }

            if(TitleBar is null)
            {
                var titlebar = new DefaultTitleBar();
                TitleBar = titlebar;
                BindingOperations.SetBinding(TitleBar, DefaultTitleBar.TitleProperty, new Binding { Path = new PropertyPath(TitleProperty), Source = this });
                BindingOperations.SetBinding(TitleBar, DefaultTitleBar.IconProperty, new Binding { Path = new PropertyPath(IconProperty), Source = this });
                
                Icon defaulticon = SystemIcons.Application;
                BitmapSource imgsource = Imaging.CreateBitmapSourceFromHIcon(defaulticon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                
                titlebar.Icon = Icon is null ? imgsource : Icon;
            }

            if(CommandButtons is null)
            {
                CommandButtons = new DefaultCommandButtons();
                
            }


            var titleBar = GetTemplateChild("Titlebar") as FrameworkElement;
            titleBar.MouseLeftButtonDown += TitleBar_MouseLeftButtonDown;

        }

#pragma warning disable 618
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                e.Handled = true;

                // taken from DragMove internal code
                VerifyAccess();

                // for the touch usage
                UnsafeNativeMethods.ReleaseCapture();

                var criticalHandle = (IntPtr)_criticalHandlePropertyInfo.GetValue(this, _emptyObjectArray);

                var wpfPoint = PointToScreen(Mouse.GetPosition(this));
                var x = (int)wpfPoint.X;
                var y = (int)wpfPoint.Y;
                NativeMethods.SendMessage(criticalHandle, WM.NCLBUTTONDOWN, (IntPtr)HT.CAPTION, new IntPtr(x | (y << 16)));
            }
            else if (e.ClickCount == 2
                     && ResizeMode != ResizeMode.NoResize)
            {
                e.Handled = true;

                if (WindowState == WindowState.Normal
                    && ResizeMode != ResizeMode.NoResize
                    && ResizeMode != ResizeMode.CanMinimize)
                {
                    SystemCommands.MaximizeWindow(this);
                }
                else
                {
                    SystemCommands.RestoreWindow(this);
                }
            }
        }
#pragma warning restore 618


        private void OnContentRendered(object sender, EventArgs e)
        {
            ContentRendered -= OnContentRendered;

            InitializeGlowWindowBehavior();
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

        private void InitializeGlowWindowBehavior()
        {
            var behavior = new GlowWindowBehavior();
            BindingOperations.SetBinding(behavior, GlowWindowBehavior.ResizeBorderThicknessProperty, new Binding { Path = new PropertyPath(ResizeBoarderThicknessProperty), Source = this });
            BindingOperations.SetBinding(behavior, GlowWindowBehavior.GlowBrushProperty, new Binding { Path = new PropertyPath(WindowGlowProperty), Source = this });
            BindingOperations.SetBinding(behavior, GlowWindowBehavior.NonActiveGlowBrushProperty, new Binding { Path = new PropertyPath(NonActiveWindowGlowProperty), Source = this });

            Interaction.GetBehaviors(this).Add(behavior);
        }
    }
}
