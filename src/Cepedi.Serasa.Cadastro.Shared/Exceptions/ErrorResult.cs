using Cepedi.Serasa.Cadastro.Shared.Enums;

namespace Cepedi.Serasa.Cadastro.Shared.Exceptions
{
    public class ErrorResult
    {
        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public ErrorType Type { get; set; }
    }
}
