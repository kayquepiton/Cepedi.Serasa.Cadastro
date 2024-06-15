using Cepedi.Serasa.Cadastro.Shared.Enums;

namespace Cepedi.Serasa.Cadastro.Shared.Exececoes;
public class SemResultadoExcecao : ExcecaoAplicacao
{
    public SemResultadoExcecao() : 
        base(CadastroErros.SemResultados)
    {
    }
}
