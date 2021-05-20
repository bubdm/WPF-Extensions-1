using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using WPF_Extension.Bindings;
using WPF_Extension.Bindings.NotifyPropertyChanged;
using WPF_Extension.Theming;

namespace WPF_Extension.Playground
{
    public class ViewModel : ObservableObject
    {
        private AdvancedTextBoxBinding _textBoxBinding;

        public AdvancedTextBoxBinding TextBoxBinding
        {
            get => _textBoxBinding;
            set => Set(value, ref _textBoxBinding);
        }

        private ObservableCollection<string> _items;

        public ObservableCollection<string> Items
        {
            get => _items;
            set => Set(value, ref _items);
        }

 
        public ViewModel()
        {

            TextBoxBinding = new AdvancedTextBoxBinding(new BindingParameter()
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            Items = new ObservableCollection<string>()
            {
                "Test",
                "Hallo",
                "Tee",
                "Tee Baum",
                "Buchen Baum"
            };

            TextBoxBinding.Suggestions = Items;
        }

    }
}
