using DDDProject.Domain;
using DDDProject.Domain.Entities;

namespace Wpm.Mangament.Api.Application;

public record CreatePetCommand(
        Guid Id,
        string Name,
        int Age,
        string Color,
        SexOfPet SexOfPet,
        Guid BreedId
    );