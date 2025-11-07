using Wpm.SharedKernel;

namespace DDDProject.Domain.Events;

public static class DomainEvents
{
    public static DomainEventDispatcher<PetWeightUpdated> PetWeightUpdated { get; } = new();
}