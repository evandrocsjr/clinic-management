namespace Wpm.Clinic.Domain.ValuesObjects;

public class PatientId
{
    public Guid Value { get; private set; }

    public PatientId(Guid value)
    {
        if(value == Guid.Empty)
            throw new ArgumentNullException(nameof(value), "The indetifier is not valid");
        
        Value = value;
    }

    public static implicit operator PatientId(Guid value)
    {
        return new PatientId(value);
    }
}