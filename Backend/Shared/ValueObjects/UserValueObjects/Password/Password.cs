using Shared.Common;
using Shared.ValueObjects.Common;

namespace Shared;

public class Password : ValueObject
{
    public string Value { get; }
    private const int MaxLength = 500;

    public Password(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new InvalidValueException(value, nameof(Password));
        }

        Value = value;
    }

    public static implicit operator Password(string value) =>
        value is null ? null : new Password(value);

    public static implicit operator string(Password value) => value?.Value;
}
