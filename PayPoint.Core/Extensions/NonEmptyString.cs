namespace PayPoint.Core.Extensions;

public class NonEmptyString
{
    public string Value { get; }

    public NonEmptyString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El valor no puede ser nulo o vacío", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;
}
