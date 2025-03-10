namespace Domain.Abstractions;

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    public ulong Version { get; protected set; }

    private readonly Queue<IDomainEvent> _events = new();

    public void LoadFromHistory(IEnumerable<IDomainEvent> history)
    {
        foreach (var @event in history)
        {
            Apply(@event);
            Version++;
        }
    }
    protected void RaiseEvent<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
        Apply(@event);
        _events.Enqueue(@event);
    }
    protected abstract void Apply(IDomainEvent @event);
    public void DequeueUncommittedEvents()
        => _events.Clear();

    public IEnumerable<IDomainEvent> GetUncommittedEvents()
        => _events.AsEnumerable();
}