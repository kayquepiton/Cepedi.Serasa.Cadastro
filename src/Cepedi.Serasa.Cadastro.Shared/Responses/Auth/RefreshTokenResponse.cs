namespace Cepedi.Serasa.Cadastro.Shared.Auth.Responses
{
    public record RefreshTokenResponse(bool Authenticated, DateTime Created, DateTime AccessTokenExpiration, DateTime RefreshTokenExpiration, string AccessToken, string RefreshToken);
}
