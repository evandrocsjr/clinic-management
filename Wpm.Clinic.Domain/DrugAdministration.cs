using DDDProject.Domain;
using Wpm.Clinic.Domain.ValuesObjects;

namespace Wpm.Clinic.Domain;

public class DrugAdministration : Entity
{
    public DrugId DrugId { get; init; }
    
    public Dose Dose { get; init; }

    public DrugAdministration(DrugId drugId, Dose dose)
    {
        Id = Guid.NewGuid();
        DrugId = drugId;
        Dose = dose;
    }
}