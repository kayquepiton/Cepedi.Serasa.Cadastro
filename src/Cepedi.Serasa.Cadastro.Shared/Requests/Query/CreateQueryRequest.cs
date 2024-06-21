using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using OperationResult;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Query
{
    public class CreateQueryRequest : IRequest<Result<CreateQueryResponse>>, IValidate
    {
        public required DateTime Date { get; set; }
        public required bool Status { get; set; }
        public required int PersonId { get; set; }
    }
}
