namespace Wpm.SharedKernel;

public record Weight
{
    public decimal Value { get; init; }

    public Weight(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Weigth value can't be negative");
        Value = value;
    }

    public static implicit operator Weight(decimal value)
    {
        return new Weight(value);
    }
    
    public static implicit operator decimal(Weight value) => value.Value;
}