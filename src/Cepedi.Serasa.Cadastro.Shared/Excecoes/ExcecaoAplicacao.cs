namespace Cepedi.Serasa.Cadastro.Shared.Exececoes;
public class ExcecaoAplicacao : Exception
{
    public ExcecaoAplicacao(ResultadoErro erro)
     : base(erro.Descricao) => ResultadoErro = erro;

    public ResultadoErro ResultadoErro { get; set; }
}
