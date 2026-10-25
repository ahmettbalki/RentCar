using FluentValidation;
using TS.MediatR;

namespace RentCarServer.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, new()
{
    private readonly IValidator<TRequest>[] _validators =
        validators.ToArray();

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        if (_validators.Length == 0)
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(x =>
                x.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .DistinctBy(x => new { x.PropertyName, x.ErrorMessage })
            .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}
