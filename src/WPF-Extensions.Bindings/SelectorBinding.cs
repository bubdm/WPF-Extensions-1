using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPF_Extensions.Bindings
{
    public class SelectorBinding : ItemsControlBinding
    {

        private object _selecteditem;

        public object SelectedItem
        {
            get => _selecteditem;
            set => Set(value, ref _selecteditem);
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => Set(value, ref _selectedIndex);
        }



        public SelectorBinding(BindingParameter bindingParameter)
            : base(bindingParameter)
        {

        }


        public override void BindProperties(DependencyObject dependencyObject)
        {
            if (dependencyObject is not Selector)
                throw new ArgumentException($"UI-Element must be an {typeof(Selector).FullName}");

            base.BindProperties(dependencyObject);

            Bind(Selector.SelectedIndexProperty, dependencyObject, nameof(SelectedIndex));
            Bind(Selector.SelectedItemProperty, dependencyObject, nameof(SelectedItem));
        }
    }
}
