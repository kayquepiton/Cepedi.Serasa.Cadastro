namespace Cepedi.Serasa.Cadastro.Domain.Configuration
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInMinutesAccessToken { get; set; }
        public int ExpirationInMinutesRefreshToken { get; set; }
    }
}
