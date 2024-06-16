using Cepedi.Serasa.Cadastro.Shared.Responses.Query;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Query
{
    public class GetAllQueriesRequest : IRequest<Result<List<GetAllQueriesResponse>>>
    {
    }
}
