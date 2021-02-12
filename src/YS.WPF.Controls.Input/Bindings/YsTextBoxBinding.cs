using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using YS.WPF.Controls.Input;

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


        private string _assistentText;

        public string AssistentText
        {
            get => _assistentText;
            set => Set(value, ref _assistentText);
        }

        private Brush _assistentTextForeground;

        public Brush AssistentTextForeground
        {
            get => _assistentTextForeground;
            set => Set(value, ref _assistentTextForeground);
        }


        public YsTextBoxBinding(BindingParameter bindingParameter)
            :base(bindingParameter)
        {

        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            if (dependencyObject is not YsTextBox)
                throw new ArgumentException($"UI-Element must be an {typeof(YsTextBox).FullName}");

            base.BindProperties(dependencyObject);

            Bind(YsTextBox.WatermarkProperty, dependencyObject, nameof(Watermark));
            Bind(YsTextBox.LabelProperty, dependencyObject, nameof(Label));
            Bind(YsTextBox.LabelForegorundProperty, dependencyObject, nameof(LabelForeground));
            Bind(YsTextBox.AssistentTextProperty, dependencyObject, nameof(AssistentText));
            Bind(YsTextBox.AssistenTextForegroundProperty, dependencyObject, nameof(AssistentTextForeground));

        }
    }

    public class YsTextBoxBinding<T> : TextBoxBinding<T>
        where T : IConvertible
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


        private string _assistentText;

        public string AssistentText
        {
            get => _assistentText;
            set => Set(value, ref _assistentText);
        }

        private Brush _assistentTextForeground;

        public Brush AssistentTextForeground
        {
            get => _assistentTextForeground;
            set => Set(value, ref _assistentTextForeground);
        }

        public YsTextBoxBinding(BindingParameter bindingParameter)
            : base(bindingParameter)
        {

        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            if (dependencyObject is not YsTextBox)
                throw new ArgumentException($"UI-Element must be an {typeof(YsTextBox).FullName}");

            base.BindProperties(dependencyObject);

            Bind(YsTextBox.WatermarkProperty, dependencyObject, nameof(Watermark));
            Bind(YsTextBox.LabelProperty, dependencyObject, nameof(Label));
            Bind(YsTextBox.LabelForegorundProperty, dependencyObject, nameof(LabelForeground));
            Bind(YsTextBox.AssistentTextProperty, dependencyObject, nameof(AssistentText));
            Bind(YsTextBox.AssistenTextForegroundProperty, dependencyObject, nameof(AssistentTextForeground));

        }
    }
}
