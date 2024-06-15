using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
public class DeletarConsultaRequest : IRequest<Result<DeletarConsultaResponse>>
{
    public int Id { get; set; }
}
