using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace YS.WPF.Controls.Bindings
{
    public class BindingParameter
    {
        public BindingMode Mode { get; set; }

        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        public bool NotifyOnSourceUpdated { get; set; }

        public bool NotifyOnTargetUpdated { get; set; }

        public bool NotifyOnValidationError { get; set; }
        public bool NotifyOnDataErrors { get; set set; }

        public BindingParameter()
        {
            
        }
    }
}