﻿using Shared.Common;
using Shared.ValueObjects.Common;
using System.Text.RegularExpressions;

namespace Shared;

public class Email : ValueObject
{
    private static readonly Regex Regex =
        new(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
                + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.Compiled
        );

    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException(value, nameof(Email));
        }

        if (value.Length > 100)
        {
            throw new InvalidValueException(value, nameof(Email));
        }

        value = value.ToLowerInvariant();
        if (!Regex.IsMatch(value))
        {
            throw new InvalidValueException(value, nameof(Email));
        }

        Value = value;
    }

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string email) => new Email(email);

    public override string ToString() => Value;
}
