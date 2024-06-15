using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Movimentacao;
public class CriarMovimentacaoRequestHandler : IRequestHandler<CriarMovimentacaoRequest, Result<CriarMovimentacaoResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger<CriarMovimentacaoRequestHandler> _logger;

    public CriarMovimentacaoRequestHandler(ILogger<CriarMovimentacaoRequestHandler> logger, IMovimentacaoRepository movimentacaoRepository, IPessoaRepository pessoaRepository, ITipoMovimentacaoRepository tipoMovimentacaoRepository)
    {
        _pessoaRepository = pessoaRepository;
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<CriarMovimentacaoResponse>> Handle(CriarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        // Verificar se a pessoa existe
        var pessoa = await _pessoaRepository.ObterPessoaAsync(request.IdPessoa);
        if (pessoa == null)
        {
            // Se a pessoa não existe, retornar um erro indicando a falta de resultados
            return Result.Error<CriarMovimentacaoResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdPessoaInvalido));
        }

        // Verificar se o tipo de movimentação existe
        var tipoMovimentacao = await _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
        if (tipoMovimentacao == null)
        {
            // Se o tipo de movimentação não existe, retornar um erro indicando a falta de resultados
            return Result.Error<CriarMovimentacaoResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdTipoMovimentacaoInvalido));
        }

        // Criar uma nova instância de MovimentacaoEntity com base nos Data da solicitação
        var movimentacao = new MovimentacaoEntity()
        {
            IdTipoMovimentacao = request.IdTipoMovimentacao,
            IdPessoa = request.IdPessoa,
            DataHora = DateTime.UtcNow,
            NomeEstabelecimento = request.NomeEstabelecimento,
            Valor = request.Valor,
        };

        // Persistir a nova movimentação no repositório
        await _movimentacaoRepository.CriarMovimentacaoAsync(movimentacao);

        // Criar uma resposta com os Data da movimentação criada
        var response = new CriarMovimentacaoResponse(
            movimentacao.Id,
            movimentacao.IdTipoMovimentacao,
            movimentacao.IdPessoa,
            movimentacao.DataHora,
            movimentacao.NomeEstabelecimento,
            movimentacao.Valor
        );

        // Retornar um resultado de sucesso contendo a resposta da movimentação criada
        return Result.Success(response);
    }
}

