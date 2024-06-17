using System.Security.Cryptography;
using System.Text;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using Cepedi.Serasa.Cadastro.Shared.User.Requests;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.User.Handlers
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
    {
        private readonly ILogger<CreateUserRequestHandler> _logger;
        private readonly IUserRepository _userRepository;

        public CreateUserRequestHandler(IUserRepository userRepository, ILogger<CreateUserRequestHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                return Result.Error<CreateUserResponse>(new Shared.Exceptions.AppException(RegistrationErrors.DuplicateUsername));
            }

            string hashedPassword;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
                hashedPassword = Convert.ToBase64String(hashedBytes);
            }

            var user = new UserEntity()
            {
                Name = request.Name,
                Username = request.Username,
                Password = hashedPassword
            };

            await _userRepository.CreateUserAsync(user);

            return Result.Success(new CreateUserResponse(user.Id, user.Name));
        }
    }
}
