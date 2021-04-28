using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_Extensions.Controls
{
    public class DefaultCommandButtons : UserControl
    {
        private Window _parentWindow;

        private Button _maxRestoreButton;
        private Button _closeButton;
        private Button _minimizeButton;

        static DefaultCommandButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DefaultCommandButtons), new FrameworkPropertyMetadata(typeof(DefaultCommandButtons)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _parentWindow = GetParentWindow();

            _closeButton = GetTemplateChild("CloseButton") as Button;
            _minimizeButton = GetTemplateChild("MinimizeButton") as Button;
            _maxRestoreButton = GetTemplateChild("MaxRestoreButton") as Button;

            _closeButton.Click += (s, e) => _parentWindow?.Close();
            _minimizeButton.Click += (s, e) => _parentWindow.WindowState = WindowState.Minimized;
            _maxRestoreButton.Click += (s, e) => _parentWindow.WindowState = _parentWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

            ResizeModeChanged(null, null);

            _parentWindow.Loaded += ParentWindow_Loaded;

            _parentWindow.StateChanged += (s, e) =>
            {
                switch (_parentWindow.WindowState)
                {
                    case WindowState.Normal:
                        _maxRestoreButton.Content = "\uE739";
                        break;
                    case WindowState.Maximized:
                        _maxRestoreButton.Content = "\uE923";
                        break;
                    default:
                        break;
                }
            };
        }

        private void ParentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var resizeModeDescriptor = DependencyPropertyDescriptor.FromProperty(Window.ResizeModeProperty, typeof(AdvancedWindow));
            resizeModeDescriptor.AddValueChanged(_parentWindow, ResizeModeChanged);
        }

        private void ResizeModeChanged(object sender, EventArgs e)
        {
            switch (_parentWindow.ResizeMode)
            {
                case ResizeMode.NoResize:
                    _maxRestoreButton.Visibility = Visibility.Collapsed;
                    _minimizeButton.Visibility = Visibility.Collapsed;
                    break;
                case ResizeMode.CanMinimize:
                    _maxRestoreButton.Visibility = Visibility.Collapsed;
                    _minimizeButton.Visibility = Visibility.Visible;
                    break;
                case ResizeMode.CanResize:
                    _maxRestoreButton.Visibility = Visibility.Visible;
                    _minimizeButton.Visibility = Visibility.Visible;
                    break;
                case ResizeMode.CanResizeWithGrip:
                    _maxRestoreButton.Visibility = Visibility.Visible;
                    _minimizeButton.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
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
