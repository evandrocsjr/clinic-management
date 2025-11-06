namespace Wpm.Clinic.Domain.ValuesObjects;

public record Dose
{
    public decimal Quantity { get; init; }
    
    public UnitOfMeasure Unit { get; init; }

    public Dose(decimal quantity, UnitOfMeasure unit)
    {
        Quantity = quantity;
        Unit = unit;
    }
};

public enum UnitOfMeasure
{
    mg,
    ml,
    tablet
}