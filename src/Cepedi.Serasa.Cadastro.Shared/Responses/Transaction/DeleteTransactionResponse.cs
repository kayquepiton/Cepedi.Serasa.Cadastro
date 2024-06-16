namespace Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;

public record DeleteTransactionResponse(int Id, int TransactionTypeId, int PersonId, DateTime DateTime, string? EstablishmentName, decimal Value);
