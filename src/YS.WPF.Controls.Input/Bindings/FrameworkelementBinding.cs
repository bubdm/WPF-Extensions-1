using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using YS.WPF.Controls.Input.NotifyPropertyChanged;

namespace YS.WPF.Controls.Input.Bindings
{
    public class FrameworkelementBinding : ObservableObject, IBindableObject
    {

        public BindingParameter BindingParameters { get; private set; }

        private Visibility _visibility;

        public Visibility Visibility
        {
            get => _visibility;
            set => Set(value, ref _visibility);
        }

        private bool _isEnabled;

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
            this.Bind(UIElement.VisibilityProperty, dependencyObject, nameof(Visibility), BindingParameters);
            this.Bind(UIElement.IsEnabledProperty, dependencyObject, nameof(IsEnabled), BindingParameters);
        }
    }
}
