using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Shared.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Movimentacao;
public class AtualizarMovimentacaoRequestHandler : IRequestHandler<AtualizarMovimentacaoRequest, Result<AtualizarMovimentacaoResponse>>
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger<AtualizarMovimentacaoRequestHandler> _logger;

    public AtualizarMovimentacaoRequestHandler(ITipoMovimentacaoRepository tipoMovimentacaoRepository, IMovimentacaoRepository movimentacaoRepository, ILogger<AtualizarMovimentacaoRequestHandler> logger)
    {
        _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarMovimentacaoResponse>> Handle(AtualizarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        // Verificar se o tipo de movimentação existe com base no ID fornecido na solicitação
        var tipoMovimentacao = await _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
        
        // Se o tipo de movimentação não existe, retornar um erro indicando a falta de resultados
        if (tipoMovimentacao == null)
        {
            return Result.Error<AtualizarMovimentacaoResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdTipoMovimentacaoInvalido));
        }

        // Obter a movimentação a ser atualizada com base no ID fornecido na solicitação
        var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);
        
        // Se a movimentação não existe, retornar um erro indicando a falta de resultados
        if (movimentacaoEntity == null)
        {
            return Result.Error<AtualizarMovimentacaoResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdMovimentacaoInvalido));
        }

        // Atualizar os Data da movimentação com base na solicitação
        movimentacaoEntity.IdTipoMovimentacao = request.IdTipoMovimentacao;
        movimentacaoEntity.DataHora = request.DataHora;
        movimentacaoEntity.NomeEstabelecimento = request.NomeEstabelecimento;
        movimentacaoEntity.Valor = request.Valor;

        // Persistir a movimentação atualizada no repositório
        await _movimentacaoRepository.AtualizarMovimentacaoAsync(movimentacaoEntity);

        // Criar uma resposta com os Data atualizados da movimentação
        var response = new AtualizarMovimentacaoResponse(
            movimentacaoEntity.Id,
            movimentacaoEntity.IdTipoMovimentacao,
            movimentacaoEntity.IdPessoa,
            movimentacaoEntity.DataHora,
            movimentacaoEntity.NomeEstabelecimento,
            movimentacaoEntity.Valor
        );

        // Retornar um resultado de sucesso contendo a resposta da movimentação atualizada
        return Result.Success(response);
    }
}

