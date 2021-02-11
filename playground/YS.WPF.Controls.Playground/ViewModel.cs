using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using YS.WPF.Controls.Bindings;
using YS.WPF.Controls.Bindings.NotifyPropertyChanged;
using YS.WPF.Controls.Input.ValidationRules;

namespace YS.WPF.Controls.Playground
{
    public class ViewModel : ObservableObject
    {
        private TextBoxBinding<int?> _textBoxBinding;

        public TextBoxBinding<int?> TextBoxBinding
        {
            get => _textBoxBinding;
            set => Set(value, ref _textBoxBinding);
        }

        public ViewModel()
        {
            TextBoxBinding = new TextBoxBinding<int?>(new BindingParameter()
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            })
            {
                Foreground = new SolidColorBrush(Colors.Red)
            };

            ValidationRule[] rules = new[]
            {
                new MinMaxRule(-10,100)
            };

            TextBoxBinding.AddValidationRule(rules);
        }

    }
}
