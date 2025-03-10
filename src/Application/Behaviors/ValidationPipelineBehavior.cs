using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;
        private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next();

            _logger.LogInformation("🔍 Validating request: {RequestName}", typeof(TRequest).Name);

            var context = new ValidationContext<TRequest>(request);
            var validationFailures = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (validationFailures.Any())
            {
                _logger.LogWarning("❌ Validation failed for {RequestName}: {Errors}", typeof(TRequest).Name, validationFailures);
                throw new ValidationException(validationFailures);
            }

            _logger.LogInformation("✅ Validation passed for {RequestName}", typeof(TRequest).Name);
            return await next();
        }
    }
}
