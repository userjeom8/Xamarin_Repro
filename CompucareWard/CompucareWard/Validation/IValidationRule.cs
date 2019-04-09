using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Validation
{
    public interface IValidationRule<T>
    {
        string Message { get; }
        bool Validate(T value);
    }
}
