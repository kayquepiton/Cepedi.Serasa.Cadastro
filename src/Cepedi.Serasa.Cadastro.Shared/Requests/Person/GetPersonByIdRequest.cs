using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Person
{
    public class GetPersonByIdRequest : IRequest<Result<GetPersonResponse>>
    {
        public int Id { get; set; }
    }
}
