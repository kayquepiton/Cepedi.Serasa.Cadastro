namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class MovimentacaoEntity
{
    public int Id { get; set; } 
    public int IdPessoa { get; set; }
    public PessoaEntity Pessoa { get; set; }
    public DateTime DataHora { get; set; } 
    public int IdTipoMovimentacao { get; set; }
    public TipoMovimentacaoEntity? TipoMovimentacao { get; set; }
    public decimal Valor { get; set; } 
    public string NomeEstabelecimento { get; set; } 
}

