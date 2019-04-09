using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Validation.Rules
{
    public class IsValidDecimal : IValidationRule<string>
    {
        private string _propertyName;

        public string Message { get; set; }

        public IsValidDecimal(string propertyName)
        {
            _propertyName = propertyName;
            Message = $"{_propertyName} is not a valid number";
        }

        public bool Validate(string value)
        {
            bool isValid = true;

            if (!string.IsNullOrWhiteSpace(value) && !decimal.TryParse(value, out decimal decimalValue))
                isValid = false;

            return isValid;
        }
    }
}
