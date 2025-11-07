using DDDProject.Domain;
using DDDProject.Domain.Events;
using Wpm.Mangament.Api.Infrastructure;

namespace Wpm.Mangament.Api.Application;

public class SetWeightCommandHandler : ICommandHandler<SetWeightCommand>
{
    private ManagementDbContext _dbContext;
    private IBreedService _breedService;

    public SetWeightCommandHandler(
        ManagementDbContext dbContext,
        IBreedService breedService
        )
    {
        _dbContext = dbContext;
        _breedService = breedService;
        DomainEvents.PetWeightUpdated.Subscribe((pUpdated) =>
        {
            // ACT EXECUTED
        });
    }

    public async Task Handle(SetWeightCommand command)
    {
        var pet = await _dbContext.Pets.FindAsync(command.Id);
        pet.SetWeight(command.Weight, _breedService);
        await _dbContext.SaveChangesAsync();
    }
}