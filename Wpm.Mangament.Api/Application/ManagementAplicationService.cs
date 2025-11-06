using DDDProject.Domain;
using DDDProject.Domain.Entities;
using DDDProject.Domain.ValueObjects;
using Wpm.Mangament.Api.Infrastructure;

namespace Wpm.Mangament.Api.Application;

public class ManagementAplicationService(IBreedService breedService, ManagementDbContext dbContext)
{
    public async Task Handle(CreatePetCommand command)
    {
        var breedId = new BreedId(Guid.NewGuid(), breedService);

        var newPet = new Pet(
                command.Id,
                command.Name,
                command.Age,
                command.Color,
                command.SexOfPet,
                breedId
            );
        await dbContext.Pets.AddAsync(newPet);
        await dbContext.SaveChangesAsync();
    }
}