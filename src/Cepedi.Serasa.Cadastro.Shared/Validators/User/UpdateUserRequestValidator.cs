using Cepedi.Serasa.Cadastro.Shared.User.Requests;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Domain.User.Handlers
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotNull().WithMessage("O ID é obrigatório.")
                .GreaterThan(0).WithMessage("ID inválido.");

            RuleFor(request => request.Name)
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");
        }
    }
}
