using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Requests;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers;

public class DeletarUsuarioRequestHandler : IRequestHandler<DeletarUsuarioRequest, Result<DeletarUsuarioResponse>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<DeletarUsuarioRequestHandler> _logger;

    public DeletarUsuarioRequestHandler(ILogger<DeletarUsuarioRequestHandler> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result<DeletarUsuarioResponse>> Handle(DeletarUsuarioRequest request, CancellationToken cancellationToken)
    {
        // Verificar se o usuário existe com base no ID fornecido na solicitação
        var usuarioEntity = await _usuarioRepository.ObterUsuarioAsync(request.Id);

        // Se o usuário não for encontrado, retornar um erro indicando a falta de resultados
        if (usuarioEntity == null)
        {
            return Result.Error<DeletarUsuarioResponse>(new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdUsuarioInvalido));
        }

        // Deletar o usuário do repositório
        await _usuarioRepository.DeletarUsuarioAsync(usuarioEntity.Id);

        // Criar uma resposta com os Data do usuário deletado
        var response = new DeletarUsuarioResponse(
            usuarioEntity.Id,
            usuarioEntity.Nome
        );

        // Retornar um resultado de sucesso com a resposta contendo os Data do usuário deletado
        return Result.Success(response);
    }
}

