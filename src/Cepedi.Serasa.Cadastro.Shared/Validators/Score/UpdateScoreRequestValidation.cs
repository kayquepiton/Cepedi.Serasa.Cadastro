using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Validators.Score
{
    public class UpdateScoreRequestValidation : AbstractValidator<UpdateScoreRequest>
    {
        public UpdateScoreRequestValidation()
        {
            RuleFor(score => score.Id)
                .NotNull().WithMessage("O ID deve ser fornecido.")
                .GreaterThan(0).WithMessage("ID do score inválido.");

            RuleFor(score => score.Score)
                .NotNull().WithMessage("O score deve ser fornecido.")
                .GreaterThanOrEqualTo(0).WithMessage("O valor deve ser maior ou igual a zero.")
                .LessThanOrEqualTo(1000).WithMessage("O valor deve ser menor ou igual a mil.");
        }
    }
}
