using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Validation.Rules
{
    public class PrecisionNotExceeded : IValidationRule<string>
    {
        private decimal _precision;
        private string _propertyName;

        public string Message { get; set; } = string.Empty;

        public PrecisionNotExceeded(string propertyName, int precision)
        {
            _propertyName = propertyName;
            _precision = precision;
        }

        public bool Validate(string value)
        {
            bool isValid = true;

            if (!string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, out decimal decimalValue))
            {
                if (_precision == 0 && value.Contains("."))
                {
                    isValid = false;
                    Message = $"{_propertyName} cannot have any decimal places";
                }
                else if (_precision > 0 && value.Contains(".") && value.Length > (value.IndexOf(".") + 2) && value.Substring(value.IndexOf(".") + 1).Length > _precision)
                {
                    isValid = false;
                    Message = $"{_propertyName} can only have {_precision} decimal places";
                }
            }

            return isValid;
        }
    }
}
