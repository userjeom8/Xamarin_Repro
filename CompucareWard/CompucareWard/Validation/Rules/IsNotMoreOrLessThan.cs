using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Validation.Rules
{
    public class IsNotMoreOrLessThan : IValidationRule<string>
    {
        private bool _isUpperLimit;
        private decimal _limit;
        private string _propertyName;

        public string Message { get; set; }

        public IsNotMoreOrLessThan(string propertyName, decimal limit, bool isUpperLimit)
        {
            _propertyName = propertyName;
            _limit = limit;
            _isUpperLimit = isUpperLimit;
            Message = $"{_propertyName} must be {(_isUpperLimit ? "less" : "more")} than or equal to {_limit}";
        }

        public bool Validate(string value)
        {
            bool isValid = true;

            if (!string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, out decimal decimalValue))
            {
                if (_isUpperLimit)
                    isValid = decimalValue <= _limit;
                else
                    isValid = decimalValue >= _limit;
            }

            return isValid;
        }
    }
}
