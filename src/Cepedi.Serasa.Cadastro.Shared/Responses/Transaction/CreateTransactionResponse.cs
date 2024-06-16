namespace Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;

public record CreateTransactionResponse(int Id, int TransactionTypeId, int PersonId, DateTime DateTime, string? EstablishmentName, decimal Value);
