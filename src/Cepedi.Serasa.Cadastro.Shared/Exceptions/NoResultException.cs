using Cepedi.Serasa.Cadastro.Shared.Enums;

namespace Cepedi.Serasa.Cadastro.Shared.Exceptions
{
    public class NoResultException : AppException
    {
        public NoResultException() 
            : base(new ErrorResult { Title = RegistrationErrors.NoResults.Title, Description = RegistrationErrors.NoResults.Description })
        {
        }
    }
}
