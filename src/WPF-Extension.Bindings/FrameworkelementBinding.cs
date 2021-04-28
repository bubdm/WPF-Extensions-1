using System;
using System.Windows;
using System.Windows.Data;
using WPF_Extension.Bindings.NotifyPropertyChanged;

namespace WPF_Extension.Bindings
{
    public class FrameworkelementBinding : ObservableObject
    {

        public BindingParameter BindingParameter { get; private set; }

        private Visibility _visibility;

        public Visibility Visibility
        {
            get => _visibility;
            set => Set(value, ref _visibility);
        }

        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => Set(value, ref _isEnabled);
        }



        public FrameworkelementBinding(BindingParameter bindingParameters)
        {
            BindingParameter = bindingParameters;
        }


        public virtual void BindProperties(DependencyObject dependencyObject)
        {
            if (dependencyObject is not FrameworkElement)
                throw new ArgumentException($"The UI-Element must be an {typeof(FrameworkElement)}");

            Bind(UIElement.VisibilityProperty, dependencyObject, nameof(Visibility));
            Bind(UIElement.IsEnabledProperty, dependencyObject, nameof(IsEnabled));
        }


        protected Binding Bind(DependencyProperty dependencyProperty, DependencyObject dependencyObject, string propertyName)
        {
            var propInfo = GetType().GetProperty(propertyName);
            var dpProp = dependencyObject.GetValue(dependencyProperty);
            var defaultValue = dependencyProperty.GetMetadata(dependencyObject).DefaultValue;
            var val = propInfo.GetValue(this);
            if (val == null && defaultValue != null)
                propInfo.SetValue(this, dpProp);


            if (dpProp != defaultValue)
            {
                try
                {
                    propInfo.SetValue(this, dpProp);
                }
                catch (Exception) { }
            }


            var binding = new Binding(propertyName)
            {
                Source = this,
                UpdateSourceTrigger = BindingParameter.UpdateSourceTrigger,
                NotifyOnSourceUpdated = BindingParameter.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = BindingParameter.NotifyOnTargetUpdated,
                NotifyOnValidationError = BindingParameter.NotifyOnValidationError,
                ValidatesOnNotifyDataErrors = BindingParameter.ValidatesOnNotifyDataErrors,
                ValidatesOnDataErrors = BindingParameter.ValidatesOnDataErrors,
                Mode = BindingParameter.Mode
            };

            BindingOperations.SetBinding(dependencyObject, dependencyProperty, binding);
            return binding;
        }

    }
}
