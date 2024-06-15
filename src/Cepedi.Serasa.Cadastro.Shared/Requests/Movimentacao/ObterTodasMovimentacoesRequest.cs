using Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao
{
    public class ObterTodasMovimentacoesRequest : IRequest<Result<List<ObterTodasMovimentacoesResponse>>>
    {
    }
}
