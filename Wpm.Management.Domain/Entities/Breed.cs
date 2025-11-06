using DDDProject.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace DDDProject.Domain.Entities;

public class Breed : Entity
{
    public string Name { get; private set; }
    
    public WeightRange MaleIdealWeight { get; private set; }
    
    public WeightRange FemaleIdealWeight { get; private set; }

    public Breed(
            Guid id,
            string name,
            WeightRange maleIdealWeight, 
            WeightRange femaleIdealWeight
        )
    {
        Id = id;
        Name = name;
        MaleIdealWeight = maleIdealWeight;
        FemaleIdealWeight = femaleIdealWeight;
    }
}