using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Cepedi.Serasa.Cadastro.Shared.Auth.Requests;
using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;
using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exececoes;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Domain.Services;

namespace cepedi_serasa_cadastro.Domain.Handlers.Auth;

public class AutenticarUsuarioRequestHandler : IRequestHandler<AutenticarUsuarioRequest, Result<AutenticarUsuarioResponse>>
{
    private readonly ILogger<AutenticarUsuarioRequestHandler> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public AutenticarUsuarioRequestHandler(IUsuarioRepository usuarioRepository, ILogger<AutenticarUsuarioRequestHandler> logger, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
        _tokenService = tokenService;
    }

    public async Task<Result<AutenticarUsuarioResponse>> Handle(AutenticarUsuarioRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterUsuarioPorLoginAsync(request.Login);

        if (usuario is null)
            return Result.Error<AutenticarUsuarioResponse>(new ExcecaoAplicacao(CadastroErros.AutenticacaoInvalida));
        
        if (!EncryptPassword(request.Senha).Equals(usuario.Senha))
            return Result.Error<AutenticarUsuarioResponse>(new ExcecaoAplicacao(CadastroErros.AutenticacaoInvalida));

        var (accessToken, expiresAccessToken) = GenerateAccessToken(usuario);

        var (refreshToken, expiresRefreshToken) = await GenerateRefreshTokenAndUpdateDatabaseAsync(usuario);

        var response = new AutenticarUsuarioResponse(
            Authenticated: true,
            Created: DateTime.UtcNow,
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

    /// <summary>
    /// Encrypt password method
    /// </summary>
    /// <param name="password">Password</param>
    /// <returns>Encrypted password</returns>
    private string EncryptPassword(string password)
    {
        using var sha256 = SHA256.Create();
        
        byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        string hashedPassword = Convert.ToBase64String(hashedBytes);

        return hashedPassword;
    }
}
