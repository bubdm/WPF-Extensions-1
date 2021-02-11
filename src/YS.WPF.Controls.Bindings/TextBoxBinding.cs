using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YS.WPF.Controls.Bindings
{
    public class TextBoxBinding : ControlBinding
    {

        protected DependencyObject DependencyObject;

        private string _text;

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
            Bind(TextBox.TextProperty, dependencyObject, nameof(Text), BindingParameters);
        }
    }


    public class TextBoxBinding<T> : ControlBinding
    {
        private readonly ObservableCollection<ValidationRule> _rules;
        
        private T _value;

        public T Value 
        {
            get => _value;
            set => Set(value, ref _value);
        }


        public TextBoxBinding(BindingParameter bindingParameter)
            : base(bindingParameter)
        {
            _rules = new ObservableCollection<ValidationRule>();
        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            base.BindProperties(dependencyObject);
            Bind(TextBox.TextProperty, dependencyObject, nameof(Value), BindingParameters);


            foreach (var rule in _rules)
            {
                Binding.ValidationRules.Add(rule);
            }

            _rules.CollectionChanged += (s, e) =>
            {
                if(e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (ValidationRule item in e.NewItems)
                    {
                        Binding.ValidationRules.Add(item);
                    }
                }
                

                if(e.Action == NotifyCollectionChangedAction.Remove )
                {
                    foreach (ValidationRule item in e.OldItems)
                    {
                        Binding.ValidationRules.Remove(item);
                    }
                }
            };

        }

        public void AddValidationRule(ValidationRule validationRule) 
            => _rules.Add(validationRule);

        public void AddValidationRule(IEnumerable<ValidationRule> validationRules)
        {
            BindingParameters.NotifyOnValidationError = true;
            foreach (var rule in validationRules)
            {
                _rules.Add(rule);
            }
        }
    }
}
