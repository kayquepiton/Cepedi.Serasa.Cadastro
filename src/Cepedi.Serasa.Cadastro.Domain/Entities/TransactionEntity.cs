namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class TransactionEntity
{
    public int Id { get; set; } 
    public int PersonId { get; set; } 
    public PersonEntity? Person { get; set; }
    public required DateTime DateTime { get; set; } 
    public int TransactionTypeId { get; set; } 
    public TransactionTypeEntity? TransationType { get; set; }
    public required decimal Value { get; set; } 
    public required string EstablishmentName { get; set; }
}
