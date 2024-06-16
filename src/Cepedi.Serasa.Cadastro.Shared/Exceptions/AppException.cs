namespace Cepedi.Serasa.Cadastro.Shared.Exceptions
{
    public class AppException : Exception
    {
        public AppException(ErrorResult error)
            : base(error.Description)
        {
            ErrorResult = error;
        }

        public ErrorResult ErrorResult { get; set; }
    }
}
