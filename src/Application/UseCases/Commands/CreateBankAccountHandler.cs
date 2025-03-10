using Application.Services;
using Domain.Aggregates;
using MediatR;

namespace Application.UseCases.Commands;

public sealed record CreateBankAccountCommand(decimal InitialDeposit) : IRequest<Guid>;

public class CreateBankAccountHandler(IApplicationService applicationService) : IRequestHandler<CreateBankAccountCommand, Guid>
{
    private readonly IApplicationService _applicationService = applicationService;
    public async Task<Guid> Handle(CreateBankAccountCommand command, CancellationToken cancellationToken)
    {

        var bankAccount = BankAccount.Create(command.InitialDeposit);

        await _applicationService.AppendEventsAsync(bankAccount, cancellationToken);

        return bankAccount.Id;
    }
}
