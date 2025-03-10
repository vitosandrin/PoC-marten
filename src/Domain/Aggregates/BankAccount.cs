namespace Domain.Aggregates;

using Domain.Abstractions;
using Domain.Events;

public class BankAccount : AggregateRoot
{
    public decimal Balance { get; private set; }

    public static BankAccount Create(decimal initialBalance)
    {
        var bankAccount = new BankAccount();

        bankAccount.RaiseEvent<DomainEvent.BankAccountCreated>(new(bankAccount.Id, initialBalance));

        return bankAccount;
    }
    public void Deposit(decimal amount)
        => RaiseEvent<DomainEvent.MoneyDeposited>(new(amount));

    public void Withdraw(decimal amount)
        => RaiseEvent<DomainEvent.MoneyWithdrawn>(new(amount));

    protected override void Apply(IDomainEvent @event)
            => When(@event as dynamic);

    private void When(DomainEvent.BankAccountCreated @event)
    {
        Id = @event.AccountId;
        Balance = @event.InitialBalance;
    }

    private void When(DomainEvent.MoneyDeposited @event)
        => Balance += @event.Amount;

    private void When(DomainEvent.MoneyWithdrawn @event)
        => Balance -= @event.Amount;

}