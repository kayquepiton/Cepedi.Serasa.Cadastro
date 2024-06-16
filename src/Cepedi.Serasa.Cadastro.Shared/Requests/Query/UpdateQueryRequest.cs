using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using OperationResult;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Query
{
    public class UpdateQueryRequest : IRequest<Result<UpdateQueryResponse>>, IValidate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
