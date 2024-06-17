using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType.Validators
{
    public class CreateTransactionTypeRequestValidator : AbstractValidator<CreateTransactionTypeRequest>
    {
        public CreateTransactionTypeRequestValidator()
        {
            RuleFor(transactionType => transactionType.TypeName)
                .NotEmpty()
                .WithMessage("O nome do tipo de transação é obrigatório.")
                .MinimumLength(5)
                .WithMessage("O nome do tipo de transação deve ter pelo menos 5 caracteres.")
                .MaximumLength(30)
                .WithMessage("O nome do tipo de transação deve ter no máximo 30 caracteres.");
        }
    }
}
