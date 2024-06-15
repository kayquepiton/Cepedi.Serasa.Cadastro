namespace Cepedi.Serasa.Cadastro.Shared.Auth.Responses;
public record AtualizarTokenResponse(bool Authenticated, DateTime Created, DateTime ExpirationAccessToken, DateTime ExpirationRefreshToken, string AccessToken, string RefreshToken);

