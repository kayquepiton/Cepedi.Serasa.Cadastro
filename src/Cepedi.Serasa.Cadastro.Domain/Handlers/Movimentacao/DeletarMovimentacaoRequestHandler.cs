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
public class DeletarMovimentacaoRequestHandler : IRequestHandler<DeletarMovimentacaoRequest, Result<DeletarMovimentacaoResponse>>
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger<DeletarMovimentacaoRequestHandler> _logger;

    public DeletarMovimentacaoRequestHandler(ILogger<DeletarMovimentacaoRequestHandler> logger, IMovimentacaoRepository movimentacaoRepository)
    {
        _logger = logger;
        _movimentacaoRepository = movimentacaoRepository;
    }

    public async Task<Result<DeletarMovimentacaoResponse>> Handle(DeletarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        // Verificar se a movimentação existe com base no ID fornecido na solicitação
        var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);

        // Se a movimentação não for encontrada, retornar um erro indicando a falta de resultados
        if (movimentacaoEntity == null)
        {
            return Result.Error<DeletarMovimentacaoResponse>(
                new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdMovimentacaoInvalido));
        }

        // Deletar a movimentação do repositório
        await _movimentacaoRepository.DeletarMovimentacaoAsync(movimentacaoEntity.Id);

        // Criar uma resposta com os Data da movimentação deletada
        var response = new DeletarMovimentacaoResponse(
            movimentacaoEntity.Id,
            movimentacaoEntity.IdTipoMovimentacao,
            movimentacaoEntity.IdPessoa,
            movimentacaoEntity.DataHora,
            movimentacaoEntity.NomeEstabelecimento,
            movimentacaoEntity.Valor
        );

        // Retornar um resultado de sucesso com a resposta contendo os Data da movimentação deletada
        return Result.Success(response);
    }
}

