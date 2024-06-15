using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Shared.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TipoMovimentacao;
public class ObterTodosTiposMovimentacaoRequestHandler : IRequestHandler<ObterTodosTiposMovimentacaoRequest, Result<List<ObterTodosTiposMovimentacaoResponse>>>
{
    private readonly ILogger<ObterTodosTiposMovimentacaoRequestHandler> _logger;
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;

    public ObterTodosTiposMovimentacaoRequestHandler(ILogger<ObterTodosTiposMovimentacaoRequestHandler> logger, ITipoMovimentacaoRepository tipoMovimentacaoRepository)
    {
        _logger = logger;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
    }

    public async Task<Result<List<ObterTodosTiposMovimentacaoResponse>>> Handle(ObterTodosTiposMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        var tiposMovimentacao = await _tipoMovimentacaoRepository.ObterTodosTiposMovimentacaoAsync();

        if (tiposMovimentacao == null)
        {
            return Result.Error<List<ObterTodosTiposMovimentacaoResponse>>(new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.ListaTiposMovimentacaoVazia));
        }

        var response = new List<ObterTodosTiposMovimentacaoResponse>();
        foreach (var tipoMovimentacao in tiposMovimentacao)
        {
            response.Add(new ObterTodosTiposMovimentacaoResponse(tipoMovimentacao.Id, tipoMovimentacao.NomeTipo));
        }

        return Result.Success(response);
    }
}
