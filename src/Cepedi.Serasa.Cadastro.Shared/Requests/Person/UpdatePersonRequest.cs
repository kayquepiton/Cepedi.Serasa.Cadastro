using Cepedi.Serasa.Cadastro.Shared.Responses.Person;
using MediatR;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Shared.Requests.Person
{
    public class UpdatePersonRequest : IRequest<Result<UpdatePersonResponse>>, IValidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
    }
}
