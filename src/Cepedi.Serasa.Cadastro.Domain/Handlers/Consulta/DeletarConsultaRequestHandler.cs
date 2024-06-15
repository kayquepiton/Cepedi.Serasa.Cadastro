using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Consulta;
public class DeletarConsultaRequestHandler :
    IRequestHandler<DeletarConsultaRequest, Result<DeletarConsultaResponse>>
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly ILogger<DeletarConsultaRequestHandler> _logger;

    public DeletarConsultaRequestHandler(IConsultaRepository consultaRepository, ILogger<DeletarConsultaRequestHandler> logger)
    {
        _consultaRepository = consultaRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarConsultaResponse>> Handle(DeletarConsultaRequest request, CancellationToken cancellationToken)
    {
        var consulta = await _consultaRepository.ObterConsultaAsync(request.Id);

        if (consulta == null)
        {
            return Result.Error<DeletarConsultaResponse>(new Shared.
                Exececoes.ExcecaoAplicacao(CadastroErros.IdConsultaInvalido));
        }

        await _consultaRepository.DeletarConsultaAsync(consulta.Id);

        var response = new DeletarConsultaResponse(consulta.Id,
                                                    consulta.IdPessoa,
                                                    consulta.Status,
                                                    consulta.Data);

        return Result.Success(response);
    }
}