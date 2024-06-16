using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Transaction
{
    public class CreateTransactionRequest : IRequest<Result<CreateTransactionResponse>>, IValidate
    {
        public int IdTransactionType { get; set; }
        public int IdPerson { get; set; }
        public DateTime DateTime { get; set; }
        public string EstablishmentName { get; set; } = default!;
        public decimal Value { get; set; }
    }
}
