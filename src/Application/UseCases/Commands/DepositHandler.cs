using Application.Services;
using Domain.Aggregates;
using MediatR;

namespace Application.UseCases.Commands;

public sealed record DepositCommand(Guid BankAccountId, decimal Amount) : IRequest;

public class DepositHandler(IApplicationService applicationService) : IRequestHandler<DepositCommand>
{
    private readonly IApplicationService _applicationService = applicationService;
    public async Task Handle(DepositCommand command, CancellationToken cancellationToken)
    {
        var bankAccount = await _applicationService.LoadAggregateAsync<BankAccount>(command.BankAccountId, cancellationToken);

        bankAccount.Deposit(command.Amount);

        await _applicationService.AppendEventsAsync(bankAccount, cancellationToken);
    }
}
