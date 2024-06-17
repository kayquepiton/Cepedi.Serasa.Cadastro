using Cepedi.Serasa.Cadastro.Shared.User.Requests;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Domain.User.Handlers
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

            RuleFor(request => request.Username)
                .NotEmpty().WithMessage("O usuário é obrigatório.");

            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.");
        }
    }
}
