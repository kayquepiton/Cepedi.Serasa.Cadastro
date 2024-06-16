namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class TransactionEntity
{
    public int Id { get; set; } 
    public int IdPerson { get; set; } 
    public PersonEntity Person { get; set; }
    public DateTime DateTime { get; set; } 
    public int IdTransactionType { get; set; } 
    public TransactionTypeEntity TransationType { get; set; }
    public decimal Value { get; set; } 
    public string EstablishmentName { get; set; }
}
