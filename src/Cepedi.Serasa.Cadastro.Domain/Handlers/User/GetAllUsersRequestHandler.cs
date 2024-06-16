using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.User.Requests;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers
{
    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, Result<List<GetAllUsersResponse>>>
    {
        private readonly ILogger<GetAllUsersRequestHandler> _logger;
        private readonly IUserRepository _userRepository;

        public GetAllUsersRequestHandler(ILogger<GetAllUsersRequestHandler> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Result<List<GetAllUsersResponse>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();

            if (users is null)
            {
                return Result.Error<List<GetAllUsersResponse>>(
                    new Shared.Exceptions.AppException(RegistrationErrors.EmptyUserList));
            }

            var response = new List<GetAllUsersResponse>();

            foreach (var user in users)
            {
                response.Add(new GetAllUsersResponse(
                    user.Id,
                    user.Name,
                    user.Login
                ));
            }

            return Result.Success(response);
        }
    }
}
