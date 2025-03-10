using Domain.Abstractions;

namespace Application.Services;

public interface IApplicationService
{
    Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken)
        where TAggregate : class, IAggregateRoot, new();

    Task AppendEventsAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot;
}
