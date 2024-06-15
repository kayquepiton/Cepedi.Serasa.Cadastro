using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
public class AtualizarConsultaRequest : IRequest<Result<AtualizarConsultaResponse>>, IValida
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public bool Status { get; set; }

}
