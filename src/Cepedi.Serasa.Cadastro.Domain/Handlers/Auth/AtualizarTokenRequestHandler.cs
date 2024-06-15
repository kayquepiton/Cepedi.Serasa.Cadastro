using Cepedi.Serasa.Cadastro.Shared.Auth.Requests;
using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Security.Claims;
using Cepedi.Serasa.Cadastro.Domain.Services;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exececoes;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Auth;

public class AtualizarTokenRequestHandler : IRequestHandler<AtualizarTokenRequest, Result<AtualizarTokenResponse>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<AtualizarTokenRequestHandler> _logger;
    private readonly ITokenService _tokenService;

    public AtualizarTokenRequestHandler(IUsuarioRepository usuarioRepository, ILogger<AtualizarTokenRequestHandler> logger, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
        _tokenService = tokenService;
    }

    public async Task<Result<AtualizarTokenResponse>> Handle(AtualizarTokenRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterUsuarioPorRefreshTokenAsync(request.RefreshToken);
        
        if (usuario is null)
            return Result.Error<AtualizarTokenResponse>(new ExcecaoAplicacao(CadastroErros.AutenticacaoInvalida));

        if (DateTime.UtcNow >= usuario.ExpirationRefreshToken)
            return Result.Error<AtualizarTokenResponse>(new ExcecaoAplicacao(CadastroErros.AutenticacaoInvalida));
        
        var (accessToken, expiresAccessToken) = GenerateAccessToken(usuario);

        var (refreshToken, expiresRefreshToken) = await GenerateRefreshTokenAndUpdateDatabaseAsync(usuario);

        var response = new AtualizarTokenResponse(
            Authenticated: true,
            Created:  DateTime.UtcNow,
            ExpirationAccessToken: expiresAccessToken,
            ExpirationRefreshToken: expiresRefreshToken,
            AccessToken: accessToken,
            RefreshToken: refreshToken
        );

        return Result.Success(response);
    }
    
    /// <summary>
    /// Generate refresh token and update database
    /// </summary>
    /// <param name="usuario">User entity</param>
    /// <returns>RefreshToken and Expires RefreshToken</returns>
    private async Task<(string, DateTime)> GenerateRefreshTokenAndUpdateDatabaseAsync(UsuarioEntity usuario)
    {
        var (refreshToken, expiresRefreshToken) = _tokenService.GenerateRefreshToken();

        usuario.RefreshToken = refreshToken;
        usuario.ExpirationRefreshToken = expiresRefreshToken;
        
        await _usuarioRepository.AtualizarUsuarioAsync(usuario);

        return (refreshToken, expiresRefreshToken);
    }
    
    /// <summary>
    /// Generate AccessToken
    /// </summary>
    /// <param name="usuario">User entity</param>
    /// <returns>Accesstoken and expires accesstoken</returns>
    private (string, DateTime) GenerateAccessToken(UsuarioEntity usuario)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, usuario.Login),
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString())
        };
        
        return _tokenService.GenerateAccessToken(claims);
    }
}
