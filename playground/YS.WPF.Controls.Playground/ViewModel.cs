using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using YS.WPF.Controls.Bindings;
using YS.WPF.Controls.Bindings.NotifyPropertyChanged;

namespace YS.WPF.Controls.Playground
{
    public class ViewModel : ObservableObject
    {
        private TextBoxBinding _textBoxBinding;

        public TextBoxBinding TextBoxBinding
        {
            get => _textBoxBinding;
            set => Set(value, ref _textBoxBinding);
        }

        public ViewModel()
        {
            TextBoxBinding = new TextBoxBinding(new BindingParameter()
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            })
            {
                Foreground = new SolidColorBrush(Colors.Red)
            };
        }

    }
}
