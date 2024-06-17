using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Validators.Score
{
    public class CreateScoreRequestValidation : AbstractValidator<CreateScoreRequest>
    {
        public CreateScoreRequestValidation()
        {
            RuleFor(score => score.PersonId)
                .NotNull().WithMessage("O ID da pessoa deve ser fornecido.")
                .GreaterThan(0).WithMessage("ID da pessoa inválido.");

            RuleFor(score => score.Score)
                .NotNull().WithMessage("O score deve ser fornecido.")
                .GreaterThanOrEqualTo(0).WithMessage("O valor deve ser maior ou igual a zero.")
                .LessThanOrEqualTo(1000).WithMessage("O valor deve ser menor ou igual a mil.");
        }
    }
}
