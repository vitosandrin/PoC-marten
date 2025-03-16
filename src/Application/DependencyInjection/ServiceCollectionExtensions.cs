using Application.Behaviors;
using Application.Services;
using FluentValidation;
using Infrastructure.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        })
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddExceptionHandler<CustomExceptionHandler>()
            .AddScoped<IApplicationService, ApplicationService>();

}
