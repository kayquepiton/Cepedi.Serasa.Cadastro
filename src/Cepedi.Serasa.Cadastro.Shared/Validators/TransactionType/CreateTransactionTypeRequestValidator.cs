using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType.Validators
{
    public class CreateTransactionTypeRequestValidator : AbstractValidator<CreateTransactionTypeRequest>
    {
        public CreateTransactionTypeRequestValidator()
        {
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
