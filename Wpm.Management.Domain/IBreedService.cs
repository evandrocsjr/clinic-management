using DDDProject.Domain.Entities;
using DDDProject.Domain.ValueObjects;

namespace DDDProject.Domain;

public interface IBreedService
{
    Breed? GetBreed(Guid id);
}

public class FakeBreedService : IBreedService
{
    public List<Breed> Breeds = [
        new Breed(Guid.NewGuid(), "Raça 1", new WeightRange(20m, 30m), new WeightRange(20m, 40m)),
        new Breed(Guid.NewGuid(), "Raça 2", new WeightRange(20m, 30m), new WeightRange(40m, 40m)),
    ];

    public Breed? GetBreed(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Breed is not valid.");
        var breed = Breeds.FirstOrDefault(b => b.Id == id);
        return breed ?? throw new ArgumentException("Breed not exists.");
    }
}

public interface IManagementRepository
{
    Pet? GetById(Guid id);
    IEnumerable<Pet> GetAll();
    void Insert(Pet pet);
    void Update(Pet pet);
    void Delete(Pet pet);
}