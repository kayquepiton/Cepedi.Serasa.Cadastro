namespace Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
public record UpdateTransactionResponse(int Id, int TransactionTypeId, int PersonId, DateTime DateTime, string? EstablishmentName, decimal Value);
