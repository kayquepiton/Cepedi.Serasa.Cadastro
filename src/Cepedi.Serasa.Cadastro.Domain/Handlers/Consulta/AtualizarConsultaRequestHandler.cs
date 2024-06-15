using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Consulta;
public class AtualizarConsultaRequestHandler : IRequestHandler<AtualizarConsultaRequest, Result<AtualizarConsultaResponse>>
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly ILogger<AtualizarConsultaRequestHandler> _logger;

    public AtualizarConsultaRequestHandler(IConsultaRepository consultaRepository, ILogger<AtualizarConsultaRequestHandler> logger)
    {
        _consultaRepository = consultaRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarConsultaResponse>> Handle(AtualizarConsultaRequest request, CancellationToken cancellationToken)
    {
        var consulta = await _consultaRepository.ObterConsultaAsync(request.Id);

        if (consulta == null)
        {
            return Result.Error<AtualizarConsultaResponse>(new Shared.
                Exececoes.ExcecaoAplicacao(CadastroErros.IdConsultaInvalido));
        }

        consulta.Status = request.Status;

        await _consultaRepository.AtualizarConsultaAsync(consulta);
        
        var response = new AtualizarConsultaResponse(consulta.Id, 
                                                    consulta.IdPessoa, 
                                                    consulta.Status, 
                                                    consulta.Data);

        return Result.Success(response);
    }
}
