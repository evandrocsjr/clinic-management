namespace Wpm.SharedKernel;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> changes = new();
    
    public int Version { get; private set; }
    
    public IReadOnlyCollection<IDomainEvent> GetChanges => changes.AsReadOnly();
    
    public void ClearChanges() => changes.Clear();

    protected void ApplyDomainEvent(IDomainEvent domainEvent)
    {
        ChangeStateByUsingDomainEvent(domainEvent);
        changes.Add(domainEvent);
        Version++;
    }

    public void Load(IEnumerable<IDomainEvent> history)
    {
        foreach (var h in history)
        {
            ApplyDomainEvent(h);
        }
        ClearChanges();
    }
    
    protected abstract void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent);
}