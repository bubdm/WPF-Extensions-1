using System;
using System.Globalization;
using System.Windows.Controls;

namespace WPF_Extensions.Bindings.ValidationRules
{
    public class MinMaxValidation : ValidationRule
    {
        private double _min;
        private double _max;

        public MinMaxValidation(double min = double.NegativeInfinity, double max = double.PositiveInfinity)
        {
            _min = min;
            _max = max;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (value is not null and not string)
                return new ValidationResult(false, "Input must be a string");

            var input = (string)value;

            if (string.IsNullOrEmpty(input))
                return ValidationResult.ValidResult;

            if (!double.TryParse(input, out var number))
                return new ValidationResult(false, "Input must be a number");

            if (number < _min || number > _max)
                return new ValidationResult(false, $"Number must be in a range of {_min}-{_max}");

            return ValidationResult.ValidResult;


        }
    }
}
