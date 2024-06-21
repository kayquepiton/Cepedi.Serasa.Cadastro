namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class UserEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; } 
    public string? RefreshToken { get; set; } 
    public DateTime ExpirationRefreshToken { get; set; } 
}

