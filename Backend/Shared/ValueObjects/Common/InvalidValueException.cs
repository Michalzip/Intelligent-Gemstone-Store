using System;

namespace Shared.ValueObjects.Common
{
    public class InvalidValueException : Exception
    {
        public object InvalidValue { get; }

        public InvalidValueException(object value, string nameOfValueObject)
            : base($"Value: '{value}' is invalid for {nameOfValueObject}.")
        {
            InvalidValue = value;
        }
    }
}
