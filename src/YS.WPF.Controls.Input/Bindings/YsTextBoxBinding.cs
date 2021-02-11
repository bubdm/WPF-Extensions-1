using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace YS.WPF.Controls.Bindings
{
    public class YsTextBoxBinding : TextBoxBinding
    {

        private string _watermark;

        public string Watermark
        {
            get => _watermark;
            set => Set(value, ref _watermark);
        }

        private string _label;

        public string Label
        {
            get => _label;
            set => Set(value, ref _label);
        }

        private Brush _labelForeground;

        public Brush LabelForeground
        {
            get => _labelForeground;
            set => Set(value, ref _labelForeground);
        }



        public YsTextBoxBinding(BindingParameter bindingParameter)
            :base(bindingParameter)
        {

        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            base.BindProperties(dependencyObject);

        }
    }
}
