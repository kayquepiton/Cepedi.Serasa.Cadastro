namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class PessoaEntity
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string CPF { get; set; }
    public ScoreEntity Score { get; set; }
}
