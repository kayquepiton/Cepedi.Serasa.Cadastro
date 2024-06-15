using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Consulta
{
    public class ObterTodasConsultasRequest : IRequest<Result<List<ObterTodasConsultasResponse>>>
    {
    }
}
