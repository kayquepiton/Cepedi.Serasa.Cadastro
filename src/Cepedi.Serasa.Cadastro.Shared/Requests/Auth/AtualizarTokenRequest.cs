using MediatR;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Auth.Responses;
using System;

namespace Cepedi.Serasa.Cadastro.Shared.Auth.Requests
{
    public class AtualizarTokenRequest : IRequest<Result<AtualizarTokenResponse>>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
