using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exececoes;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.Extensions.Logging;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Auth.Requests;
using Cepedi.Serasa.Cadastro.Domain.Services;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Auth;
public class RevogarTokenRequestHandler : IRequestHandler<RevogarTokenRequest, Result>
{
    private readonly ILogger<RevogarTokenRequestHandler> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public RevogarTokenRequestHandler(ILogger<RevogarTokenRequestHandler> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result> Handle(RevogarTokenRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterUsuarioPorRefreshTokenAsync(request.RefreshToken);

        if (usuario is null)
            return Result.Error(new ExcecaoAplicacao(CadastroErros.AutenticacaoInvalida));
        
        usuario.RefreshToken = null;
        usuario.ExpirationRefreshToken = DateTime.UtcNow;
        
        await _usuarioRepository.AtualizarUsuarioAsync(usuario);

        return Result.Success();
    }
}