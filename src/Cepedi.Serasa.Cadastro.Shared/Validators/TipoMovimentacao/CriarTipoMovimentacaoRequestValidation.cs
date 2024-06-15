using Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Shared.Validators.TipoMovimentacao;

public class CriarTipoMovimentacaoRequestValidation : AbstractValidator<CriarTipoMovimentacaoRequest>
{
    public CriarTipoMovimentacaoRequestValidation()
    {
        RuleFor(tipoMovimentacao => tipoMovimentacao.NomeTipo)
            .NotEmpty()
            .WithMessage("O nome do tipo de movimentação é obrigatório.")
            .MinimumLength(5)
            .WithMessage("O nome do tipo de movimentação deve ter pelo menos 5 caracteres.")
            .MaximumLength(30)
            .WithMessage("O nome do tipo de movimentação deve ter até 5 caracteres.");
    }
}
