using FluentValidation;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction.Validators
{
    public class UpdateTransactionRequestValidation : AbstractValidator<UpdateTransactionRequest>
    {
        public UpdateTransactionRequestValidation()
        {
            RuleFor(transaction => transaction.Id)
                .NotNull().WithMessage("ID must be provided.")
                .GreaterThan(0).WithMessage("Invalid transaction ID.");

            RuleFor(transaction => transaction.IdTransactionType)
                .NotNull().WithMessage("Transaction type ID must be provided.")
                .GreaterThan(0).WithMessage("Invalid transaction type ID.");

            RuleFor(transaction => transaction.DateTime)
                .NotEmpty().WithMessage("Date and time must be provided.")
                .Must(dateTime => dateTime != default(DateTime)).WithMessage("Date and time must be valid.");

            RuleFor(transaction => transaction.EstablishmentName)
                .NotEmpty().WithMessage("Establishment name must be provided.")
                .MinimumLength(3).WithMessage("Establishment name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Establishment name must not exceed 100 characters.");

            RuleFor(transaction => transaction.Value)
                .NotEmpty().WithMessage("Value must be provided.")
                .GreaterThanOrEqualTo(0).WithMessage("Invalid transaction value.");
        }
    }
}
