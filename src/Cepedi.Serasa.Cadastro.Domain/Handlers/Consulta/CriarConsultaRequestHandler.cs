using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Consulta;
public class CriarConsultaRequestHandler
    : IRequestHandler<CriarConsultaRequest, Result<CriarConsultaResponse>>
{
    private readonly ILogger<CriarConsultaRequestHandler> _logger;
    private readonly IConsultaRepository _consultaRepository;

    public CriarConsultaRequestHandler(IConsultaRepository consultaRepository, IPessoaRepository _pessoaRepository, ILogger<CriarConsultaRequestHandler> logger)
    {
        _consultaRepository = consultaRepository;
        _pessoaRepository = _pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<CriarConsultaResponse>> Handle(CriarConsultaRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await _consultaRepository.ObterPessoaConsultaAsync(request.IdPessoa);
        if (pessoa == null)
        {
            return Result.Error<CriarConsultaResponse>(new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdPessoaInvalido));
        }

        var consulta = new ConsultaEntity()
        {
            Status = request.Status,
            Data = request.Data,
            IdPessoa = request.IdPessoa,
        };

        await _consultaRepository.CriarConsultaAsync(consulta);


        var response = new CriarConsultaResponse(consulta.Id,
                                                consulta.IdPessoa,
                                                consulta.Status,
                                                consulta.Data);
        return Result.Success(response);
    }
}