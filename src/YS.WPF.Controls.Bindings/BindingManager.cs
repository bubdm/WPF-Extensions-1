using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace WPF_Extensions.Bindings
{
    public class BindingManager : DependencyObject
    {
        public static readonly DependencyProperty BindingProperty = DependencyProperty.RegisterAttached("Binding",
            typeof(FrameworkelementBinding), typeof(BindingManager), new PropertyMetadata(null, OnBindingChanged));

        public static FrameworkelementBinding GetBinding(DependencyObject dobj) 
            => (FrameworkelementBinding)dobj.GetValue(BindingProperty);

        public static void SetBinding(DependencyObject dobj, FrameworkelementBinding binding)
            => dobj.SetValue(BindingProperty, binding);


        private static void OnBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var binding = (FrameworkelementBinding)e.NewValue;
            binding.BindProperties(d);
        }
    }
}
