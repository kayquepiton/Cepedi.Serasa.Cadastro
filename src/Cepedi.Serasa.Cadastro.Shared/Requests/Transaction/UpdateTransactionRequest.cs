using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using MediatR;
using OperationResult;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction
{
    public class UpdateTransactionRequest : IRequest<Result<UpdateTransactionResponse>>, IValidate
    {
        public int Id { get; set; }
        public int IdTransactionType { get; set; }
        public DateTime DateTime { get; set; }
        public string EstablishmentName { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}
