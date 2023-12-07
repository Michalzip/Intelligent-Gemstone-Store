using Shared.Common;
using Shared.ValueObjects.Common;

namespace Shared;

public class City : ValueObject
{
    public string Value { get; }

    public City(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException(value, nameof(City));
        }

        if (value.Length > 100)
        {
            throw new InvalidValueException(value, nameof(City));
        }
        Value = value;
    }

    public static implicit operator string(City city) => city.Value;

    public static implicit operator City(string city) => new City(city);

    public override string ToString() => Value;
}
