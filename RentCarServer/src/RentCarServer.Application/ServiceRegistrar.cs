using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentCarServer.Application.Behaviors;
using TS.MediatR;

namespace RentCarServer.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(crf =>
        {
            crf.RegisterServicesFromAssembly(typeof(ServiceRegistrar).Assembly);
            crf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            crf.AddOpenBehavior(typeof(PermissionBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(ServiceRegistrar).Assembly);

        return services;
    }
}