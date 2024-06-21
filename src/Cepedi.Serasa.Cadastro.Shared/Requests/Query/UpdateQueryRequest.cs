using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using OperationResult;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Query
{
    public class UpdateQueryRequest : IRequest<Result<UpdateQueryResponse>>, IValidate
    {
        public required int Id { get; set; }
        public required DateTime Date { get; set; }
        public required bool Status { get; set; }
    }
}
