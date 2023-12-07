using Shared.Common;
using Shared.ValueObjects.Common;

namespace Shared;

public class Street : ValueObject
{
    public string Value { get; }

    public Street(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException(value, nameof(Street));
        }

        Value = value;
    }

    public static implicit operator Street(string value) => new Street(value);

    public static implicit operator string(Street value) => value.Value;
}
