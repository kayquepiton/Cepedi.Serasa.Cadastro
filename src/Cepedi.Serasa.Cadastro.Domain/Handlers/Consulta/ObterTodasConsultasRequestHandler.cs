using Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Consulta;
public class ObterTodasConsultasRequestHandler : IRequestHandler<ObterTodasConsultasRequest, Result<List<ObterTodasConsultasResponse>>>
{
    private readonly ILogger<ObterTodasConsultasRequestHandler> _logger;
    private readonly IConsultaRepository _consultaRepository;

    public ObterTodasConsultasRequestHandler(ILogger<ObterTodasConsultasRequestHandler> logger, IConsultaRepository consultaRepository)
    {
        _logger = logger;
        _consultaRepository = consultaRepository;
    }

    public async Task<Result<List<ObterTodasConsultasResponse>>> Handle(ObterTodasConsultasRequest request, CancellationToken cancellationToken)
    {
        var consultas = await _consultaRepository.ObterTodasConsultasAsync();

        if (consultas == null)
        {
            return Result.Error<List<ObterTodasConsultasResponse>>(new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.ListaConsultasVazia));
        }

        var response = new List<ObterTodasConsultasResponse>();
        foreach (var consulta in consultas)
        {
            response.Add(new ObterTodasConsultasResponse(consulta.Id,
                                                        consulta.IdPessoa,
                                                        consulta.Status,
                                                        consulta.Data));
        }

        return Result.Success(response);
    }
}
