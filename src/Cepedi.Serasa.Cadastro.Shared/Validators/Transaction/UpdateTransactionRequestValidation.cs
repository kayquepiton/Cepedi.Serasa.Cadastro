using FluentValidation;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction.Validators
{
    public class UpdateTransactionRequestValidation : AbstractValidator<UpdateTransactionRequest>
    {
        public UpdateTransactionRequestValidation()
        {
            RuleFor(transaction => transaction.Id)
                .NotNull().WithMessage("O ID deve ser fornecido.")
                .GreaterThan(0).WithMessage("ID de transação inválido.");

            RuleFor(transaction => transaction.TransactionTypeId)
                .NotNull().WithMessage("O ID do tipo de transação deve ser fornecido.")
                .GreaterThan(0).WithMessage("ID do tipo de transação inválido.");

            RuleFor(transaction => transaction.DateTime)
                .NotEmpty().WithMessage("A data e hora devem ser fornecidas.")
                .Must(dateTime => dateTime != default(DateTime)).WithMessage("A data e hora devem ser válidas.");

            RuleFor(transaction => transaction.EstablishmentName)
                .NotEmpty().WithMessage("O nome do estabelecimento deve ser fornecido.")
                .MinimumLength(3).WithMessage("O nome do estabelecimento deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome do estabelecimento não deve exceder 100 caracteres.");

            RuleFor(transaction => transaction.Value)
                .NotEmpty().WithMessage("O valor deve ser fornecido.")
                .GreaterThanOrEqualTo(0).WithMessage("Valor da transação inválido.");
        }
    }
}
