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
                .WithMessage("ID é obrigatório.");

            RuleFor(person => person.Name)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .MinimumLength(3)
                .WithMessage("O nome deve ter pelo menos 3 caracteres.");

            RuleFor(person => person.CPF)
                .Matches("^[0-9]{11}$")
                .WithMessage("O CPF deve conter 11 dígitos de 0 a 9.");
        }
    }
}
