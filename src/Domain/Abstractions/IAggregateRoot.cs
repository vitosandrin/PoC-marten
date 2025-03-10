namespace Domain.Abstractions;

public interface IAggregateRoot : IEntity
{
    ulong Version { get; }
    void LoadFromHistory(IEnumerable<IDomainEvent> history);
    void DequeueUncommittedEvents();
    IEnumerable<IDomainEvent> GetUncommittedEvents();
}