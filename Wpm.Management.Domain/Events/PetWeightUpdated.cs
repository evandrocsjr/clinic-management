using Wpm.SharedKernel;

namespace DDDProject.Domain.Events;

public record PetWeightUpdated(Guid Id, decimal Weight) : IDomainEvent;