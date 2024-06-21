using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.User.Responses;

namespace Cepedi.Serasa.Cadastro.Shared.User.Requests
{
    public class DeleteUserByIdRequest : IRequest<Result<DeleteUserByIdResponse>>
    {
        public int Id { get; set; }
    }
}
