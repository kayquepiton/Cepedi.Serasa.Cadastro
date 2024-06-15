namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class UsuarioEntity
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string RefreshToken { get; set; } 
    public DateTime ExpirationRefreshToken { get; set; }
}

