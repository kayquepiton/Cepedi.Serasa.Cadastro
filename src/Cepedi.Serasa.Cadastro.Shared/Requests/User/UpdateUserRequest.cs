using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.User.Requests
{
    public class UpdateUserRequest : IRequest<Result<UpdateUserResponse>>
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
