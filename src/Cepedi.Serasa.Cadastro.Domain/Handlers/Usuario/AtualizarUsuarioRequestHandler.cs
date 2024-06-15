using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Responses;
using Cepedi.Serasa.Cadastro.Shared.Usuario.Requests;
using Cepedi.Serasa.Cadastro.Shared.Enums;

namespace Cepedi.Serasa.Cadastro.Domain.Usuario.Handlers;

public class AtualizarUsuarioRequestHandler : IRequestHandler<AtualizarUsuarioRequest, Result<AtualizarUsuarioResponse>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<AtualizarUsuarioRequestHandler> _logger;

    public AtualizarUsuarioRequestHandler(IUsuarioRepository usuarioRepository, ILogger<AtualizarUsuarioRequestHandler> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarUsuarioResponse>> Handle(AtualizarUsuarioRequest request, CancellationToken cancellationToken)
    {
        var usuarioEntity = await _usuarioRepository.ObterUsuarioAsync(request.Id);

        if (usuarioEntity == null)
        {
            return Result.Error<AtualizarUsuarioResponse>(
            new Shared.Exececoes.ExcecaoAplicacao(CadastroErros.IdUsuarioInvalido));
        }

        usuarioEntity.Nome = request.Nome ?? usuarioEntity.Nome;
        
        await _usuarioRepository.AtualizarUsuarioAsync(usuarioEntity);

        return Result.Success(new AtualizarUsuarioResponse(usuarioEntity.Nome));
    }
}

