using Application.Responses;
using Application.Services;
using Domain.Aggregates;
using MediatR;

namespace Application.UseCases.Queries;

public sealed record GetBankAccountByIdQuery(Guid BankAccountId) : IRequest<BankAccountResponse>;

public class GetBankAccountByIdHandler(IApplicationService applicationService) : IRequestHandler<GetBankAccountByIdQuery, BankAccountResponse>
{
    private readonly IApplicationService _applicationService = applicationService;
    public async Task<BankAccountResponse> Handle(GetBankAccountByIdQuery query, CancellationToken cancellationToken)
        => await _applicationService.LoadAggregateAsync<BankAccount>(query.BankAccountId, cancellationToken);

}
