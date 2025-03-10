using Domain.Abstractions;

namespace Domain.Events;
public static class DomainEvent
{
    public record BankAccountCreated(Guid AccountId, decimal InitialBalance) : IDomainEvent;
    public record MoneyDeposited(decimal Amount) : IDomainEvent;
    public record MoneyWithdrawn(decimal Amount) : IDomainEvent;
}
