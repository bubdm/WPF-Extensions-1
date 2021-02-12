using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YS.WPF.Controls.Bindings
{
    public class ControlBinding : FrameworkelementBinding
    {

        private Brush _foreground;

        public Brush Foreground
        {
            get => _foreground;
            set => Set(value, ref _foreground);
        }

        private Brush _background;

        public ControlBinding(BindingParameter bindingParameters) 
            : base(bindingParameters)
        {
        }

        public Brush Background
        {
            get => _background;
            set => Set(value, ref _background);
        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            if (dependencyObject is not Control)
                throw new ArgumentException($"The UI-Element musst be an {typeof(Control)}");

            base.BindProperties(dependencyObject);

            Bind(Control.ForegroundProperty, dependencyObject, nameof(Foreground));
            Bind(Control.BackgroundProperty, dependencyObject, nameof(Background));
        }
    }
}
