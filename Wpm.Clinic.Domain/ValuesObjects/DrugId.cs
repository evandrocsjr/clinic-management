namespace Wpm.Clinic.Domain.ValuesObjects;

public record DrugId
{
    public Guid Value { get; init; }

    public DrugId(Guid value)
    {
        Value = value;
    }
};