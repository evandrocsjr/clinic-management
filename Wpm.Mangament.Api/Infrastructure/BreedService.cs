using DDDProject.Domain;
using DDDProject.Domain.ValueObjects;

namespace Wpm.Management.Api.Infrastructure;

public class BreedService : IBreedService
{
    public List<Breed> Breeds = [
        new Breed(Guid.Parse("abc2131231-asdasd-213212"), "Raça 1", new WeightRange(20m, 30m), new WeightRange(20m, 40m)),
        new Breed(Guid.Parse("abc2131231-asdasd-213213"), "Raça 2", new WeightRange(20m, 30m), new WeightRange(40m, 40m)),
    ];

    public Breed? GetBreed(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Breed is not valid.");
        var breed = Breeds.FirstOrDefault(b => b.Id == id);
        return breed ?? throw new ArgumentException("Breed not exists.");
    }
}