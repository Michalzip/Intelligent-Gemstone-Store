using Shared.Common;
using Shared.ValueObjects.Common;
using System.Text.RegularExpressions;

namespace Shared;

public class HouseNumber : ValueObject
{
    public string Value { get; }

    public HouseNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException(value, nameof(HouseNumber));
        }

        if (!IsValidHouseNumberFormat(value))
        {
            throw new InvalidValueException(value, nameof(HouseNumber));
        }

        Value = value;
    }

    private bool IsValidHouseNumberFormat(string houseNumber)
    {
        return Regex.IsMatch(houseNumber, @"^[0-9A-Za-z]+$");
    }

    public static implicit operator HouseNumber(string value) => new(value);

    public static implicit operator string(HouseNumber houseNumber) => houseNumber.Value;

    public override string ToString() => Value;
}
