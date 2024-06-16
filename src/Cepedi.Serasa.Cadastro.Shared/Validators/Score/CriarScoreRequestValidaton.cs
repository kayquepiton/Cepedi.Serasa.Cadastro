using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Validators.Score
{
    public class CreateScoreRequestValidation : AbstractValidator<CreateScoreRequest>
    {
        public CreateScoreRequestValidation()
        {
            RuleFor(score => score.IdPerson)
                .NotNull().WithMessage("Person ID must be provided")
                .GreaterThan(0).WithMessage("Invalid Person ID");

            RuleFor(score => score.Score)
                .NotNull().WithMessage("Score must be provided")
                .GreaterThanOrEqualTo(0).WithMessage("Value must be greater than or equal to zero")
                .LessThanOrEqualTo(1000).WithMessage("Value must be less than or equal to one thousand");
        }
    }
}
