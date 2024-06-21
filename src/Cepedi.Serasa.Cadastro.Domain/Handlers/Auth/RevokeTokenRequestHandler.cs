using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Microsoft.Extensions.Logging;
using Cepedi.Serasa.Cadastro.Shared.Auth.Requests;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Auth
{
    public class RevokeTokenRequestHandler : IRequestHandler<RevokeTokenRequest, Result>
    {
        private readonly ILogger<RevokeTokenRequestHandler> _logger;
        private readonly IUserRepository _userRepository;

        public RevokeTokenRequestHandler(ILogger<RevokeTokenRequestHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(RevokeTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);

            if (user is null)
                return Result.Error(new Shared.Exceptions.AppException(RegistrationErrors.InvalidAuthentication));
            
            user.RefreshToken = null;
            user.ExpirationRefreshToken = DateTime.UtcNow;
            
            await _userRepository.UpdateUserAsync(user);

            return Result.Success();
        }
    }
}
