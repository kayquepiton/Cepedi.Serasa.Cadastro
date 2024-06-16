using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Person
{
    public class GetAllPersonsRequest : IRequest<Result<IEnumerable<GetPersonResponse>>>
    {
    }
}
