using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Person
{
    public class UpdatePersonRequest : IRequest<Result<UpdatePersonResponse>>, IValidate
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string CPF { get; set; }
    }
}
