using System;
using System.Collections.Generic;
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
        private Button _maxRestoreButton;
        private Window _parentWindow;

        static DefaultCommandButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DefaultCommandButtons), new FrameworkPropertyMetadata(typeof(DefaultCommandButtons)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _parentWindow = GetParentWindow();

            var closeButton = GetTemplateChild("CloseButton") as Button;
            var minimizeButton = GetTemplateChild("MinimizeButton") as Button;
            _maxRestoreButton = GetTemplateChild("MaxRestoreButton") as Button;

            closeButton.Click += (s, e) => _parentWindow?.Close();
            minimizeButton.Click += (s, e) => _parentWindow.WindowState = WindowState.Minimized;
            _maxRestoreButton.Click += (s, e) => _parentWindow.WindowState = _parentWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;


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
