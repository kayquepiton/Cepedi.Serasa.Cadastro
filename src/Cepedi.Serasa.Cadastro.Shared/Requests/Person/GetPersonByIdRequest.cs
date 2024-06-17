using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Person
{
    public class GetPersonByIdRequest : IRequest<Result<GetPersonByIdResponse>>
    {
        public int Id { get; set; }
    }
}
