using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using MediatR;
using OperationResult;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction
{
    public class UpdateTransactionRequest : IRequest<Result<UpdateTransactionResponse>>, IValidate
    {
        public required int Id { get; set; }
        public required int TransactionTypeId { get; set; }
        public required DateTime DateTime { get; set; }
        public required string EstablishmentName { get; set; } = string.Empty;
        public required decimal Value { get; set; }
    }
}
