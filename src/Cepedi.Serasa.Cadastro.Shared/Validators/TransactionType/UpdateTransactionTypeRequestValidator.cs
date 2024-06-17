using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType.Validators
{
    public class UpdateTransactionTypeRequestValidator : AbstractValidator<UpdateTransactionTypeRequest>
    {
        public UpdateTransactionTypeRequestValidator()
        {
            RuleFor(transactionType => transactionType.Id)
                .NotNull().WithMessage("O ID deve ser fornecido.")
                .GreaterThan(0).WithMessage("ID de tipo de transação inválido.");

            RuleFor(transactionType => transactionType.TypeName)
                .NotEmpty().WithMessage("O nome do tipo de transação é obrigatório.")
                .MinimumLength(5).WithMessage("O nome do tipo de transação deve ter pelo menos 5 caracteres.")
                .MaximumLength(30).WithMessage("O nome do tipo de transação deve ter no máximo 30 caracteres.");
        }
    }
}
