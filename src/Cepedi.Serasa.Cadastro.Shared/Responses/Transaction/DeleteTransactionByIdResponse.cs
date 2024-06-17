namespace Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;

public record DeleteTransactionByIdResponse(int Id, int TransactionTypeId, int PersonId, DateTime DateTime, string? EstablishmentName, decimal Value);
