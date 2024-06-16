using Cepedi.Serasa.Cadastro.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cepedi.Serasa.Cadastro.Shared.Exceptions
{
    public class InvalidRequestException : AppException
    {
        public InvalidRequestException(IDictionary<string, string[]> errors)
            : base(RegistrationErrors.InvalidData)
        {
            Errors = errors.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}");
        }

        public IEnumerable<string> Errors { get; }
    }
}
