using FluentValidation;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Query.Validators
{
    public class CreateQueryRequestValidator : AbstractValidator<CreateQueryRequest>
    {
        public CreateQueryRequestValidator()
        {
            RuleFor(query => query.IdPerson)
                .NotNull().WithMessage("The ID is required.")
                .GreaterThan(0).WithMessage("Invalid query ID.");

            RuleFor(query => query.Status)
                .NotNull().WithMessage("Status is required.")
                .Must(status => status == true || status == false).WithMessage("Status must be true or false");

            RuleFor(query => query.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(dateTime => dateTime != default(DateTime)).WithMessage("Date must be valid");
        }
    }
}
