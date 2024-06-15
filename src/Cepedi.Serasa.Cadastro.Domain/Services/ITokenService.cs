using System.Security.Claims;

namespace Cepedi.Serasa.Cadastro.Domain.Services;
public interface ITokenService
{
    (string, DateTime) GenerateAccessToken(IEnumerable<Claim> claims);
    (string, DateTime) GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}

