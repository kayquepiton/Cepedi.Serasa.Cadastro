using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Shared.Usuario.Requests
{
    public class ObterTodosUsuariosRequest : IRequest<Result<List<ObterTodosUsuariosResponse>>> { }
}
