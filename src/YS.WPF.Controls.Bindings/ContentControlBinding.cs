using System;
using System.Windows;
using System.Windows.Controls;

namespace YS.WPF.Controls.Bindings
{
    public class ContentControlBinding : ControlBinding
    {

        private object _content;

        public object Content
        {
            get => _content;
            set => Set(value, ref _content);
        }


        public ContentControlBinding(BindingParameter bindingParameter)
            :base(bindingParameter)
        {

        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            if (dependencyObject is not ContentControl)
                throw new ArgumentException($"UI-Element must be an {typeof(ContentControl).FullName}");

            base.BindProperties(dependencyObject);

            Bind(ContentControl.ContentProperty, dependencyObject, nameof(Content));
        }
    }
}
