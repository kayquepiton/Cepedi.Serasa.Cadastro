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

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Auth
{
    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, Result<RefreshTokenResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RefreshTokenRequestHandler> _logger;
        private readonly ITokenService _tokenService;

        public RefreshTokenRequestHandler(IUserRepository userRepository, ILogger<RefreshTokenRequestHandler> logger, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
            
            if (user is null)
                return Result.Error<RefreshTokenResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidAuthentication));

            if (DateTime.UtcNow >= user.ExpirationRefreshToken)
                return Result.Error<RefreshTokenResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidAuthentication));
            
            var (accessToken, expiresAccessToken) = GenerateAccessToken(user);

            var (refreshToken, expiresRefreshToken) = await GenerateRefreshTokenAndUpdateDatabaseAsync(user);

            var response = new RefreshTokenResponse(
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
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            
            return _tokenService.GenerateAccessToken(claims);
        }
    }
}
