namespace Cepedi.Serasa.Cadastro.Domain.Entities;

public class QueryEntity{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public PersonEntity Person { get; set; }
    public required DateTime Date { get; set; }
    public required bool Status { get; set; }
}
