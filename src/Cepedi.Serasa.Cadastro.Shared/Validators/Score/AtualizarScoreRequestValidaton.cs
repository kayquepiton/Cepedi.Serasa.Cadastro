using Cepedi.Serasa.Cadastro.Shared.Requests.Score;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Validators.Score
{
    public class UpdateScoreRequestValidation : AbstractValidator<UpdateScoreRequest>
    {
        public UpdateScoreRequestValidation()
        {
            RuleFor(score => score.Id)
                .NotNull().WithMessage("The ID must be provided")
                .GreaterThan(0).WithMessage("Invalid score ID");

            RuleFor(score => score.Score)
                .NotNull().WithMessage("The Score must be provided")
                .GreaterThanOrEqualTo(0).WithMessage("The value must be greater than or equal to zero")
                .LessThanOrEqualTo(1000).WithMessage("The value must be less than or equal to one thousand");
        }
    }
}
