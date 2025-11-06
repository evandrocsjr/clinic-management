namespace Wpm.Clinic.Domain.ValuesObjects;

public record Text {
    public string Value { get; init; }

    public Text(string value)
    {
        ValidateValue(value);
        Value = value;
    }

    private void ValidateValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(Text), "Value cannot be null or whitespace.");
        
        if (value.Length > 500)
            throw new ArgumentException("Value cannot be longer than 500 characters.");
    }
    
    public static implicit operator Text(string value)
    {
        return new Text(value);
    }
}