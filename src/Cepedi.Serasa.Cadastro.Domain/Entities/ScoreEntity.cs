namespace Cepedi.Serasa.Cadastro.Domain.Entities;

public class ScoreEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public PersonEntity Person { get; set; }
    public required double Score { get; set; }
}
