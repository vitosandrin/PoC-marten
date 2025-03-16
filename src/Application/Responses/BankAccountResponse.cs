namespace Application.Responses;

public sealed record BankAccountResponse(Guid Id, decimal Balance)
{
    public static implicit operator BankAccountResponse(Domain.Aggregates.BankAccount bankAccount)
        => new(bankAccount.Id, bankAccount.Balance);
};
