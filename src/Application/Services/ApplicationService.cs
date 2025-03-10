using Domain.Abstractions;
using Marten;

namespace Application.Services
{
    public class ApplicationService(IDocumentStore documentStore) : IApplicationService
    {
        private readonly IDocumentStore _documentStore = documentStore;

        public async Task<TAggregate> LoadAggregateAsync<TAggregate>(Guid id, CancellationToken cancellationToken)
            where TAggregate : class, IAggregateRoot, new()
        {
            using var session = _documentStore.QuerySession();

            var events = await session.Events.FetchStreamAsync(id, token: cancellationToken);
            var domainEvents = events.Select(e => e.Data as IDomainEvent).ToList();

            return LoadAggregate<TAggregate>(domainEvents!);
        }

        public async Task AppendEventsAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken)
            where TAggregate : IAggregateRoot
        {
            using var session = _documentStore.LightweightSession();

            var uncommittedEvents = aggregate.GetUncommittedEvents();

            if (!uncommittedEvents.Any())
                return;

            var stream = await session.Events.FetchStreamAsync(aggregate.Id, token: cancellationToken);
            bool streamExists = stream != null && stream.Count > 0;

            foreach (var @event in uncommittedEvents)
            {
                if (!streamExists)
                {
                    session.Events.StartStream(aggregate.Id, @event);
                    streamExists = true;
                }
                else
                {
                    session.Events.Append(aggregate.Id, @event);
                }
            }

            await session.SaveChangesAsync(cancellationToken);

            aggregate.DequeueUncommittedEvents();
        }

        private static TAggregate LoadAggregate<TAggregate>(IReadOnlyList<IDomainEvent> events)
            where TAggregate : class, IAggregateRoot, new()
        {
            if (events == null || events.Count == 0)
                throw new InvalidOperationException($"Aggregate {typeof(TAggregate).Name} não encontrado.");

            var aggregate = new TAggregate();
            aggregate.LoadFromHistory(events);

            return aggregate;
        }
    }
}
