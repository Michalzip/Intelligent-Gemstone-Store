using Shared.Common;
using Shared.ValueObjects.Common;
using System.Text.RegularExpressions;

namespace Shared;

public class PostalCode : ValueObject
{
    public string Value { get; }

    public PostalCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException(value, nameof(PostalCode));
        }

        if (!IsValidPostalCodeFormat(value))
        {
            throw new InvalidValueException(value, nameof(PostalCode));
        }

        Value = value;
    }

    private bool IsValidPostalCodeFormat(string postalCode)
    {
        // check format "00-000"
        return Regex.IsMatch(postalCode, @"^\d{2}-\d{3}$");
    }

    public static implicit operator PostalCode(string value) => new PostalCode(value);

    public static implicit operator string(PostalCode value) => value.Value;
}
