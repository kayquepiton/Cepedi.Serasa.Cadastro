using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.User.Requests;

namespace Cepedi.Serasa.Cadastro.Domain.User.Handlers
{
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, Result<UpdateUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateUserRequestHandler> _logger;

        public UpdateUserRequestHandler(IUserRepository userRepository, ILogger<UpdateUserRequestHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<UpdateUserResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetUserAsync(request.Id);

            if (userEntity is null)
            {
                return Result.Error<UpdateUserResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidUserId));
            }

            userEntity.Name = request.Name ?? userEntity.Name;

            await _userRepository.UpdateUserAsync(userEntity);

            return Result.Success(new UpdateUserResponse(userEntity.Name));
        }
    }
}
