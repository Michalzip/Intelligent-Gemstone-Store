using System.Text.RegularExpressions;
using Shared.Common;
using Shared.ValueObjects.Common;

namespace Shared;

public class PhoneNumber : ValueObject
{
    private static readonly Regex Regex = new(@"^\+(?:[0-9] ?){6,14}[0-9]$", RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException(value, nameof(PhoneNumber));
        }

        if (value.Length > 20)
        {
            throw new InvalidValueException(value, nameof(PhoneNumber));
        }

        value = value.Trim();

        if (!Regex.IsMatch(value))
        {
            throw new InvalidValueException(value, nameof(PhoneNumber));
        }

        Value = value;
    }

    public static implicit operator PhoneNumber(string value) => new(value);

    public static implicit operator string(PhoneNumber value) => value.Value;

    public override string ToString() => Value;
}
