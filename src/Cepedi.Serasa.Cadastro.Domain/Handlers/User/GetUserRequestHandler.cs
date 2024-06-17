using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;
using Cepedi.Serasa.Cadastro.Shared.User.Requests;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.User.Handlers
{
    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, Result<GetUserByIdResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserByIdRequestHandler> _logger;

        public GetUserByIdRequestHandler(ILogger<GetUserByIdRequestHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetUserAsync(request.Id);

            if (userEntity is null)
            {
                return Result.Error<GetUserByIdResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidUserId));
            }

            var response = new GetUserByIdResponse(
                userEntity.Id,
                userEntity.Name,
                userEntity.Username
            );

            return Result.Success(response);
        }
    }
}
