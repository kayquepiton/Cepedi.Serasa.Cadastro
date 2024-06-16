using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;
using Cepedi.Serasa.Cadastro.Shared.User.Requests;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers
{
    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Result<DeleteUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteUserRequestHandler> _logger;

        public DeleteUserRequestHandler(ILogger<DeleteUserRequestHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Result<DeleteUserResponse>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetUserAsync(request.Id);

            if (userEntity == null)
            {
                return Result.Error<DeleteUserResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidUserId));
            }

            await _userRepository.DeleteUserAsync(userEntity.Id);

            var response = new DeleteUserResponse(
                userEntity.Id,
                userEntity.Name
            );

            return Result.Success(response);
        }
    }
}
