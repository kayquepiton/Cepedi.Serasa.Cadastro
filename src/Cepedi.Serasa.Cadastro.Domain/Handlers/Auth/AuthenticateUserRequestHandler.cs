using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Security.Claims;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Shared.Auth.Requests;
using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;
using Cepedi.Serasa.Cadastro.Domain.Services;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using System.Text;
using System.Security.Cryptography;

namespace cepedi_serasa_cadastro.Domain.Handlers.Auth;

public class AuthenticateUserRequestHandler : IRequestHandler<AuthenticateUserRequest, Result<AuthenticateUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthenticateUserRequestHandler> _logger;
    private readonly ITokenService _tokenService;

    public AuthenticateUserRequestHandler(ILogger<AuthenticateUserRequestHandler> logger, IUserRepository userRepository, ITokenService tokenService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<AuthenticateUserResponse>> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);

        if(user is null)
            return Result.Error<AuthenticateUserResponse>(new Cepedi.Serasa.Cadastro.Shared.Exceptions.AppException(RegistrationErrors.InvalidAuthentication));
        
        var EncryptedPassword = EncryptPassword(request.Password);
        
        if (!EncryptedPassword.Equals(user.Password))
            return Result.Error<AuthenticateUserResponse>(new Cepedi.Serasa.Cadastro.Shared.Exceptions.AppException(RegistrationErrors.InvalidAuthentication));

        var (accessToken, expiresAccessToken) = GenerateAccessToken(user);

        var (refreshToken, expiresRefreshToken) = await GenerateRefreshTokenAndUpdateDatabaseAsync(user);

        var response = new AuthenticateUserResponse(
            Authenticated: true,
            Created: DateTime.UtcNow,
            AccessTokenExpiration: expiresAccessToken,
            RefreshTokenExpiration: expiresRefreshToken,
            AccessToken: accessToken,
            RefreshToken: refreshToken
        );

        return Result.Success(response);
    }
    
    /// <summary>
    /// Generate refresh token and update database
    /// </summary>
    /// <param name="user">User entity</param>
    /// <returns>RefreshToken and Expires RefreshToken</returns>
    private async Task<(string, DateTime)> GenerateRefreshTokenAndUpdateDatabaseAsync(UserEntity user)
    {
        var (refreshToken, expiresRefreshToken) = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.ExpirationRefreshToken = expiresRefreshToken;
        
        await _userRepository.UpdateUserAsync(user);

        return (refreshToken, expiresRefreshToken);
    }
    
    /// <summary>
    /// Generate AccessToken
    /// </summary>
    /// <param name="user">User entity</param>
    /// <returns>Accesstoken and expires accesstoken</returns>
    private (string, DateTime) GenerateAccessToken(UserEntity user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
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