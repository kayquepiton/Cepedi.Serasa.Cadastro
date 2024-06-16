using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using OperationResult;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Query
{
    public class CreateQueryRequest : IRequest<Result<CreateQueryResponse>>, IValidate
    {
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public int IdPerson { get; set; }
    }
}
