using System.Windows;
using System.Windows.Controls;

namespace YS.WPF.Controls.Bindings
{
    public class TextBoxBinding : ControlBinding
    {

        private string _text;

        protected DependencyObject DependencyObject;

        public string Text
        {
            get => _text;
            set => Set(value, ref _text);
        }



        public TextBoxBinding(BindingParameter bindingParameter)
            : base(bindingParameter)
        {
        }


        public override void BindProperties(DependencyObject dependencyObject)
        {
            DependencyObject = dependencyObject;
            base.BindProperties(dependencyObject);
            this.Bind(TextBox.TextProperty, dependencyObject, nameof(Text), BindingParameters);
        }
    }


    public class TextBoxBinding<T> : ControlBinding
    {
        private T _value;

        public T Value 
        {
            get => _value;
            set => Set(value, ref _value);
        }

        public TextBoxBinding(TextBoxBindingParameter bindingParameters)
            : base(bindingParameters)
        {
        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            base.BindProperties(dependencyObject);
            this.Bind(TextBox.TextProperty, dependencyObject, nameof(Value), BindingParameters);
        }
    }
}
