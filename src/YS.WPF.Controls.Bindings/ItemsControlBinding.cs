using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YS.WPF.Controls.Bindings
{
    public class ItemsControlBinding : ControlBinding
    {

        private IEnumerable _itemsSource;

        public IEnumerable ItemsSource
        {
            get => _itemsSource;
            set => Set(value, ref _itemsSource);
        }



        public ItemsControlBinding(BindingParameter bindingParameter)
            : base(bindingParameter)
        {

        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            if (dependencyObject is not ItemsControl)
                throw new ArgumentException($"UI-Element must be an {typeof(ItemsControl).FullName}");

            base.BindProperties(dependencyObject);
            Bind(ItemsControl.ItemsSourceProperty, dependencyObject, nameof(ItemsSource));
        }
    }
}
