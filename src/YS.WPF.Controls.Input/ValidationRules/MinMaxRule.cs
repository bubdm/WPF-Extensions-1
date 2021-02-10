using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YS.WPF.Controls.Input.ValidationRules
{
    public class MinMaxRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public MinMaxRule(int min = int.MinValue, int max = int.MaxValue)
        {
            Min = min;
            Max = max;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var age = 0;

            try
            {
                if (((string)value).Length > 0)
                    age = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            return age < Min || (age > Max)
                ? new ValidationResult(false,
                  $"Please enter an age in the range: {Min}-{Max}.")
                : ValidationResult.ValidResult;
        }
    }
}
