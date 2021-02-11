using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YS.WPF.Controls.Bindings
{
    public class TextBoxBinding : ControlBinding
    {
        protected Binding _textBinding;
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
            if (dependencyObject is not TextBox)
                throw new ArgumentException($"The UI-Element musst be an {typeof(TextBox).FullName}");

            DependencyObject = dependencyObject;
            base.BindProperties(dependencyObject);
            _textBinding = Bind(TextBox.TextProperty, dependencyObject, nameof(Text));
        }
    }


    public class TextBoxBinding<T> : TextBoxBinding
        where T : IConvertible
    {
        private readonly ObservableCollection<ValidationRule> _rules;

        public T InputValue => HasValidationError || string.IsNullOrEmpty(Text) ? default : (T)Convert.ChangeType(Text, typeof(T));


        public TextBoxBinding(BindingParameter bindingParameter)
            : base(bindingParameter)
        {
            _rules = new ObservableCollection<ValidationRule>();
        }


        private bool _hasValidationError;

        public bool HasValidationError
        {
            get => _hasValidationError;
            set => Set(value, ref _hasValidationError);
        }


        public override void BindProperties(DependencyObject dependencyObject)
        {
            
            base.BindProperties(dependencyObject);

            BindingParameter.ValidatesOnDataErrors = true;
            BindingParameter.NotifyOnValidationError = true;


            Validation.AddErrorHandler(dependencyObject, (s, e) 
                => HasValidationError = e.Action == ValidationErrorEventAction.Added);

            

            foreach (var rule in _rules)
            {
                _textBinding.ValidationRules.Add(rule);
            }
            _rules.CollectionChanged += OnRulesChanged;

        }

        public void AddValidationRule(ValidationRule validationRule) 
            => _rules.Add(validationRule);

        public void AddValidationRule(IEnumerable<ValidationRule> validationRules)
        {
            BindingParameter.ValidatesOnNotifyDataErrors = true;
            foreach (var rule in validationRules)
            {
                _rules.Add(rule);
            }
        }

        private void OnRulesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ValidationRule rule in e.NewItems)
                {
                    _textBinding.ValidationRules.Add(rule);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ValidationRule rule in e.OldItems)
                {
                    _textBinding.ValidationRules.Remove(rule);
                }
            }
        }
    }
}
