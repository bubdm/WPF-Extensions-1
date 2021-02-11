using System.Windows.Data;
using YS.WPF.Controls.Bindings;
using YS.WPF.Controls.Bindings.NotifyPropertyChanged;

namespace YS.WPF.Controls.Playground
{
    public class ViewModel : ObservableObject
    {
        private TextBoxBinding<int> _textBoxBinding;

        public TextBoxBinding<int> TextBoxBinding
        {
            get => _textBoxBinding;
            set => Set(value, ref _textBoxBinding);
        }

        public ViewModel()
        {
            TextBoxBinding = new TextBoxBinding<int>(new BindingParameter()
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
        }

    }
}
