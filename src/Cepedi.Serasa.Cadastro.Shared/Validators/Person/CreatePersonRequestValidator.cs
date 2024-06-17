using Cepedi.Serasa.Cadastro.Shared.Requests.Person;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Validators.Person
{
    public class CreatePersonRequestValidator : AbstractValidator<CreatePersonRequest>
    {
        public CreatePersonRequestValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .MinimumLength(3)
                .WithMessage("O nome deve ter pelo menos 3 caracteres.");

            RuleFor(person => person.CPF)
                .NotEmpty()
                .WithMessage("CPF é obrigatório.")
                .Matches("^[0-9]{11}$")
                .WithMessage("O CPF deve conter 11 dígitos de 0 a 9.");
        }
    }
}
