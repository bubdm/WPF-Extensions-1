using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xaml;

namespace WPF_Extension.Theming
{
    public class ThemeManager
    {
        public string ThemesLocation { get; set; }

        private readonly Dictionary<string,ResourceDictionary> _themes = new();

        private ResourceDictionary _oldTheme = null;
        private ResourceDictionary _theme = null;

        public ThemeManager()
            : this("\\Themes")
        {

        }

        public ThemeManager(string themesLocation = "\\Themes")
        {
            ThemesLocation = themesLocation;
            GetThemes();
            InitTheme();
        }

        private void InitTheme()
        {
            _theme = _themes.Values.FirstOrDefault();
            if (_theme != null)
                Application.Current.Resources.MergedDictionaries.Add(_theme);
        }

        public void LoadTheme(string name)
        {
            if (!_themes.TryGetValue(name, out var dictionary))
                throw new FileNotFoundException(name);

            _oldTheme = _theme;
            _theme = dictionary;

            Application.Current.Resources.MergedDictionaries.Remove(_oldTheme);
            Application.Current.Resources.MergedDictionaries.Add(_theme);
        }

        private void GetThemes()
        {
            var appdir = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(appdir +"\\" + ThemesLocation, "*.xaml");

            foreach (var file in files)
            {

                var xamlObj = XamlServices.Parse(File.ReadAllText(file));
                if (xamlObj is ResourceDictionary resourceDictionary)
                    _themes.Add(Path.GetFileNameWithoutExtension(file),resourceDictionary);
            }
        }


    }
}
