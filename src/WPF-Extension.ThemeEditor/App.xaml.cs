using System.Windows;
using Prism.Ioc;
using WPF_Extension.ThemeEditor.Views;

namespace WPF_Extension.ThemeEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
