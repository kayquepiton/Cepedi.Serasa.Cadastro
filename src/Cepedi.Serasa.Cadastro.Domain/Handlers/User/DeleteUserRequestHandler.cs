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
    public class DeleteUserByIdRequestHandler : IRequestHandler<DeleteUserByIdRequest, Result<DeleteUserByIdResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteUserByIdRequestHandler> _logger;

        public DeleteUserByIdRequestHandler(ILogger<DeleteUserByIdRequestHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Result<DeleteUserByIdResponse>> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetUserAsync(request.Id);

            if (userEntity is null)
            {
                return Result.Error<DeleteUserByIdResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidUserId));
            }

            await _userRepository.DeleteUserAsync(userEntity.Id);

            var response = new DeleteUserByIdResponse(
                userEntity.Id,
                userEntity.Name
            );

            return Result.Success(response);
        }
    }
}
