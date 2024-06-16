namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; } 
    public string? RefreshToken { get; set; } 
    public DateTime ExpirationRefreshToken { get; set; } 
}

