using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Validators.Person
{
    public class UpdatePersonRequestValidator : AbstractValidator<UpdatePersonRequest>
    {
        public UpdatePersonRequestValidator()
        {
            RuleFor(person => person.Id)
                .NotNull()
                .WithMessage("ID is required.");

            RuleFor(person => person.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long.");

            RuleFor(person => person.CPF)
                .Matches("^[0-9]{11}$")
                .WithMessage("CPF must contain 11 digits from 0 to 9.");
        }
    }
}
