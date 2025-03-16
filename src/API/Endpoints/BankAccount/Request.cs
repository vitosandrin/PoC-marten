namespace API.Endpoints.BankAccount;

public static class Request
{
    public sealed record CreateBankAccount(decimal InitialDeposit);

    public sealed record Deposit(decimal Amount);
}
