using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace YS.WPF.Controls.Input.Bindings
{
    public class TextBoxBindingParameter : BindingParameter
    {
        public List<ValidationRule> ValidationRules { get; set; }

        public TextBoxBindingParameter()
        {
            ValidationRules = new List<ValidationRule>();
        }
    }
}
