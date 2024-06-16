using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType.Validators
{
    public class UpdateTransactionTypeRequestValidator : AbstractValidator<UpdateTransactionTypeRequest>
    {
        public UpdateTransactionTypeRequestValidator()
        {
            RuleFor(transactionType => transactionType.Id)
                    .NotNull().WithMessage("The ID must be provided.")
                    .GreaterThan(0).WithMessage("Invalid transaction type ID");

            RuleFor(transactionType => transactionType.TypeName)
                .NotEmpty()
                .WithMessage("The name of the transaction type is required.")
                .MinimumLength(5)
                .WithMessage("The name of the transaction type must be at least 5 characters.")
                .MaximumLength(30)
                .WithMessage("The name of the transaction type must be up to 30 characters.");
        }
    }
}
