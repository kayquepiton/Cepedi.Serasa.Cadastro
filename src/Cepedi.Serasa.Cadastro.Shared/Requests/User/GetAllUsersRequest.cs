using MediatR;
using OperationResult;
using System.Collections.Generic;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.User.Requests
{
    public class GetAllUsersRequest : IRequest<Result<List<GetAllUsersResponse>>>
    {
    }
}
