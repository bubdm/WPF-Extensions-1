using System;
using System.Windows;
using System.Windows.Data;
using YS.WPF.Controls.Bindings.NotifyPropertyChanged;

namespace YS.WPF.Controls.Bindings
{
    public class FrameworkelementBinding : ObservableObject
    {

        protected Binding Binding;

        public BindingParameter BindingParameters { get; private set; }

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
            BindingParameters = bindingParameters;
        }


        public virtual void BindProperties(DependencyObject dependencyObject)
        {
            Bind(UIElement.VisibilityProperty, dependencyObject, nameof(Visibility), BindingParameters);
            Bind(UIElement.IsEnabledProperty, dependencyObject, nameof(IsEnabled), BindingParameters);
        }


        protected void Bind(DependencyProperty dependencyProperty, DependencyObject dependencyObject, string propertyName, 
            BindingParameter bindingParameter)
        {
            var propInfo = GetType().GetProperty(propertyName);
            var dpProp = dependencyObject.GetValue(dependencyProperty);
            var defaultValue = dependencyProperty.GetMetadata(dependencyObject).DefaultValue;

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
                UpdateSourceTrigger = bindingParameter.UpdateSourceTrigger,
                NotifyOnSourceUpdated = bindingParameter.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = bindingParameter.NotifyOnTargetUpdated,
                NotifyOnValidationError = bindingParameter.NotifyOnValidationError,
                ValidatesOnNotifyDataErrors = bindingParameter.NotifyOnDataErrors
            };

            BindingOperations.SetBinding(dependencyObject, dependencyProperty, binding);
            Binding = binding;
        }

    }
}
