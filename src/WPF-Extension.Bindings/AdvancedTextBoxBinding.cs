using System;
using System.Collections;
using System.Windows;
using System.Windows.Media;
using WPF_Extension.Controls;

namespace WPF_Extension.Bindings
{
    public class AdvancedTextBoxBinding : TextBoxBinding
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

        private IEnumerable _suggestions;

        public IEnumerable Suggestions
        {
            get => _suggestions;
            set => Set(value, ref _suggestions);
        }


        private object _selectedItem;

        public object SelectedItem
        {
            get => _selectedItem;
            set => Set(value, ref _selectedItem);
        }



        public AdvancedTextBoxBinding(BindingParameter bindingParameter)
            :base(bindingParameter)
        {

        }

        public override void BindProperties(DependencyObject dependencyObject)
        {
            if (dependencyObject is not AdvancedTextBox)
                throw new ArgumentException($"UI-Element must be an {typeof(AdvancedTextBox).FullName}");

            base.BindProperties(dependencyObject);

            Bind(AdvancedTextBox.WatermarkProperty, dependencyObject, nameof(Watermark));
            Bind(AdvancedTextBox.LabelProperty, dependencyObject, nameof(Label));
            Bind(AdvancedTextBox.LabelForegorundProperty, dependencyObject, nameof(LabelForeground));
            Bind(AdvancedTextBox.AssistentTextProperty, dependencyObject, nameof(AssistentText));
            Bind(AdvancedTextBox.AssistenTextForegroundProperty, dependencyObject, nameof(AssistentTextForeground));
            Bind(AdvancedTextBox.SuggestionsProperty, dependencyObject, nameof(Suggestions));
            Bind(AdvancedTextBox.SelectedItemProperty, dependencyObject, nameof(SelectedItem));

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
            if (dependencyObject is not AdvancedTextBox)
                throw new ArgumentException($"UI-Element must be an {typeof(AdvancedTextBox).FullName}");

            base.BindProperties(dependencyObject);

            Bind(AdvancedTextBox.WatermarkProperty, dependencyObject, nameof(Watermark));
            Bind(AdvancedTextBox.LabelProperty, dependencyObject, nameof(Label));
            Bind(AdvancedTextBox.LabelForegorundProperty, dependencyObject, nameof(LabelForeground));
            Bind(AdvancedTextBox.AssistentTextProperty, dependencyObject, nameof(AssistentText));
            Bind(AdvancedTextBox.AssistenTextForegroundProperty, dependencyObject, nameof(AssistentTextForeground));

        }
    }
}
