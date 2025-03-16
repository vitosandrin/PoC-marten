using Application.Responses;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.BankAccount
{
    [ApiController]
    [Route("api/bank-account")]
    public class BankAccountController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Request.CreateBankAccount request, CancellationToken cancellationToken)
        {
            var command = new CreateBankAccountCommand(request.InitialDeposit);

            var bankAccountId = await _sender.Send(command, cancellationToken);

            return Created($"api/bank-account/{bankAccountId}", new IdentifierResponse(bankAccountId));
        }

        [HttpPut("{bankAccountId}/deposit")]
        public async Task<IActionResult> Deposit([FromRoute] Guid bankAccountId, [FromBody] Request.Deposit request, CancellationToken cancellationToken)
        {
            var command = new DepositCommand(bankAccountId, request.Amount);

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpGet("{bankAccountId}")]
        public async Task<IActionResult> Get([FromRoute] Guid bankAccountId, CancellationToken cancellationToken)
        {
            var query = new GetBankAccountByIdQuery(bankAccountId);

            var bankAccount = await _sender.Send(query, cancellationToken);

            return Ok(bankAccount);
        }

    }
}