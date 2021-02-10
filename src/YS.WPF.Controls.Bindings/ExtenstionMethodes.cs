using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace YS.WPF.Controls.Bindings
{
    public static class ExtenstionMethodes
    {
        public static void Bind(this IBindableObject bindableObject, DependencyProperty dp, DependencyObject depo, string propertyName, 
            BindingParameter bindingParameter)
        {
            var propInfo = bindableObject.GetType().GetProperty(propertyName);
            var dpProp = depo.GetValue(dp);
            var defaultValue = dp.GetMetadata(depo).DefaultValue;

            if(dpProp != defaultValue)
            {
                try
                {
                    propInfo.SetValue(bindableObject, dpProp);
                }
                catch (Exception ex){  }
            }

            
            var binding = new Binding(propertyName)
            { 
                Source = bindableObject,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                
            };

            if (bindingParameter is TextBoxBindingParameter textBoxBindingParameter)
            {
                foreach (var validationrule in textBoxBindingParameter.ValidationRules)
                {
                    binding.ValidationRules.Add(validationrule);
                }
            }

            BindingOperations.SetBinding(depo, dp, binding);
        }
    }
}
