using Cepedi.Serasa.Cadastro.Shared;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using FluentValidation;
using MediatR;

namespace Cepedi.Serasa.Cadastro.Domain.Pipelines;
public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IValidate
{
    private AbstractValidator<TRequest> _validator;
    public ValidationBehavior(AbstractValidator<TRequest> validator) => _validator = validator;
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await _validator.ValidateAsync(request, cancellationToken);

        if (resultadoValidacao.IsValid)
        {
            return await next.Invoke();
        }

        var context = new ValidationContext<TRequest>(request);
        var errors = resultadoValidacao
            .Errors
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (errors.Any())
        {
            throw new InvalidRequestException(errors);
        }

        return await next();
    }
}
