using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;
using Cepedi.Serasa.Cadastro.Shared.User.Requests;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.User.Handlers
{
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, Result<GetUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserRequestHandler> _logger;

        public GetUserRequestHandler(ILogger<GetUserRequestHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Result<GetUserResponse>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetUserAsync(request.Id);

            if (userEntity == null)
            {
                return Result.Error<GetUserResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidUserId));
            }

            var response = new GetUserResponse(
                userEntity.Id,
                userEntity.Name,
                userEntity.Login
            );

            return Result.Success(response);
        }
    }
}
