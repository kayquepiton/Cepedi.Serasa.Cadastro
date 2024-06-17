using FluentValidation;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Query.Validators
{
    public class CreateQueryRequestValidator : AbstractValidator<CreateQueryRequest>
    {
        public CreateQueryRequestValidator()
        {
            RuleFor(query => query.PersonId)
                .NotNull().WithMessage("O ID é obrigatório.")
                .GreaterThan(0).WithMessage("ID da consulta inválido.");

            RuleFor(query => query.Status)
                .NotNull().WithMessage("O status é obrigatório.")
                .Must(status => status == true || status == false).WithMessage("O status deve ser verdadeiro ou falso.");

            RuleFor(query => query.Date)
                .NotEmpty().WithMessage("A data é obrigatória.")
                .Must(dateTime => dateTime != default(DateTime)).WithMessage("A data deve ser válida.");
        }
    }
}
