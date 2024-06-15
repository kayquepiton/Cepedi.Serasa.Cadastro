using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository;

public interface IUsuarioRepository
{
    Task<List<UsuarioEntity>> ObterTodosUsuariosAsync();
    Task<UsuarioEntity> ObterUsuarioAsync(int id);
    Task<UsuarioEntity> ObterUsuarioPorLoginAsync(string Login); // Novo método
    Task<UsuarioEntity> ObterUsuarioPorRefreshTokenAsync(string refreshToken);
    Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity usuario);
    Task<UsuarioEntity> AtualizarUsuarioAsync(UsuarioEntity usuario);
    Task DeletarUsuarioAsync(int id);
}