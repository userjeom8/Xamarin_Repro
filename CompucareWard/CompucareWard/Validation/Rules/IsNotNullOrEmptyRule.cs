using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Validation.Rules
{
    public class IsNotNullOrEmptyRule : IValidationRule<string>
    {
        public string Message { get; set; }
        private readonly string _propertyName;

        public IsNotNullOrEmptyRule(string propertyName)
        {
            _propertyName = propertyName;
            Message = $"{_propertyName} is Mandatory";
        }

        public bool Validate(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
