using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction
{
    public class CreateTransactionRequest : IRequest<Result<CreateTransactionResponse>>, IValidate
    {
        public required int TransactionTypeId { get; set; }
        public required int PersonId { get; set; }
        public required DateTime DateTime { get; set; }
        public required string EstablishmentName { get; set; } = default!;
        public required decimal Value { get; set; }
    }
}
