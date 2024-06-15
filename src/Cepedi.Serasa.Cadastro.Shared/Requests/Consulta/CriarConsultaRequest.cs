using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
public class CriarConsultaRequest : IRequest<Result<CriarConsultaResponse>>, IValida
{
    public required DateTime Data { get; set; }
    public required bool Status { get; set; }
    public int IdPessoa { get; set; }
}
